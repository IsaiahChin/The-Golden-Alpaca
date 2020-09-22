using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Graphs;

public class SpriteGenerator2D : MonoBehaviour
{
    //Enumeration for the position of a sprite in a room. Set as flags.
    [System.Flags]
    enum SpritePositionType : short
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8
    }

    enum CellType
    {
        None,
        Room,
        Hallway
    }

    class Room
    {
        public RectInt bounds;

        public Room(Vector2Int location, Vector2Int size)
        {
            bounds = new RectInt(location, size);
        }

        public static bool Intersect(Room a, Room b)
        {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) || ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) || ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y));
        }
    }

    [SerializeField]
    Vector2Int size;
    [SerializeField]
    int roomCount;
    [SerializeField]
    Vector2Int roomMaxSize;
    //Created to make a minimum room size.
    [SerializeField]
    Vector2Int roomMinSize = new Vector2Int(1, 1);
    //Modified to hold a selection of different floor tiles.
    [SerializeField]
    GameObject[] spritePrefabs;
    [SerializeField]
    Material redMaterial;
    [SerializeField]
    Material blueMaterial;

    //Created to hold a wall tile.
    [SerializeField]
    GameObject wallPrefab;
    [SerializeField]
    Material greenMaterial;

    //Created to hol the roof tile.
    [SerializeField]
    GameObject roofPrefab;
    [SerializeField]
    GameObject[] roofPrefabs;

    Random random;
    Grid2D<CellType> grid;
    List<Room> rooms;
    Delaunay2D delaunay;
    HashSet<Prim.Edge> selectedEdges;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        random = new Random(0);
        grid = new Grid2D<CellType>(size, Vector2Int.zero);
        rooms = new List<Room>();

        //Modified to allow for the roof creation
        PlaceRooms();
        Triangulate();
        CreateHallways();
        PathfindHallways();
        PlaceRoof();
    }

    void PlaceRooms()
    {
        for (int i = 0; i < roomCount; i++)
        {
            Vector2Int location = new Vector2Int(
                random.Next(0, size.x),
                random.Next(0, size.y)
            );

            //Modified to have a minimum room size.
            Vector2Int roomSize = new Vector2Int(
                random.Next(roomMinSize.x, roomMaxSize.x + 1),
                random.Next(roomMinSize.y, roomMaxSize.y + 1)
            );

            bool add = true;
            Room newRoom = new Room(location, roomSize);
            Room buffer = new Room(location + new Vector2Int(-1, -1), roomSize + new Vector2Int(2, 2));

            foreach (var room in rooms)
            {
                if (Room.Intersect(room, buffer))
                {
                    add = false;
                    break;
                }
            }

            if (newRoom.bounds.xMin < 0 || newRoom.bounds.xMax >= size.x
                || newRoom.bounds.yMin < 0 || newRoom.bounds.yMax >= size.y)
            {
                add = false;
            }

            if (add)
            {
                rooms.Add(newRoom);
                PlaceRoom(newRoom.bounds.position, newRoom.bounds.size);

                foreach (var pos in newRoom.bounds.allPositionsWithin)
                {
                    grid[pos] = CellType.Room;
                }
            }
        }
    }

    void Triangulate()
    {
        List<Vertex> vertices = new List<Vertex>();

        foreach (var room in rooms)
        {
            vertices.Add(new Vertex<Room>((Vector2)room.bounds.position + ((Vector2)room.bounds.size) / 2, room));
        }

        delaunay = Delaunay2D.Triangulate(vertices);
    }

    void CreateHallways()
    {
        List<Prim.Edge> edges = new List<Prim.Edge>();

        foreach (var edge in delaunay.Edges)
        {
            edges.Add(new Prim.Edge(edge.U, edge.V));
        }

        List<Prim.Edge> mst = Prim.MinimumSpanningTree(edges, edges[0].U);

        selectedEdges = new HashSet<Prim.Edge>(mst);
        var remainingEdges = new HashSet<Prim.Edge>(edges);
        remainingEdges.ExceptWith(selectedEdges);

        foreach (var edge in remainingEdges)
        {
            if (random.NextDouble() < 0.125)
            {
                selectedEdges.Add(edge);
            }
        }
    }

    void PathfindHallways()
    {
        DungeonPathfinder2D aStar = new DungeonPathfinder2D(size);

        foreach (var edge in selectedEdges)
        {
            var startRoom = (edge.U as Vertex<Room>).Item;
            var endRoom = (edge.V as Vertex<Room>).Item;

            var startPosf = startRoom.bounds.center;
            var endPosf = endRoom.bounds.center;
            var startPos = new Vector2Int((int)startPosf.x, (int)startPosf.y);
            var endPos = new Vector2Int((int)endPosf.x, (int)endPosf.y);

            var path = aStar.FindPath(startPos, endPos, (DungeonPathfinder2D.Node a, DungeonPathfinder2D.Node b) => {
                var pathCost = new DungeonPathfinder2D.PathCost();

                pathCost.cost = Vector2Int.Distance(b.Position, endPos);    //heuristic

                if (grid[b.Position] == CellType.Room)
                {
                    pathCost.cost += 10;
                }
                else if (grid[b.Position] == CellType.None)
                {
                    pathCost.cost += 5;
                }
                else if (grid[b.Position] == CellType.Hallway)
                {
                    pathCost.cost += 1;
                }

                pathCost.traversable = true;

                return pathCost;
            });

            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    var current = path[i];

                    if (grid[current] == CellType.None)
                    {
                        grid[current] = CellType.Hallway;
                    }

                    if (i > 0)
                    {
                        var prev = path[i - 1];

                        var delta = current - prev;
                    }
                }

                foreach (var pos in path)
                {
                    if (grid[pos] == CellType.Hallway)
                    {
                        PlaceHallway(pos);
                    }
                }
            }
        }
    }

    //Modified to work with sprites instead of cubes.
    void PlaceFloorSprite(Vector2Int location, Vector2Int size, Material material)
    {
        GameObject go = Instantiate(NextSprite(), SpriteFloorLocationFix(size, location), Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
        //Rotate sprite to be flat, then a random 90 degree rotation on the ground.
        go.GetComponent<Transform>().rotation = Quaternion.Euler(90, random.Next(0, 4) * 90, 0);
        go.GetComponent<SpriteRenderer>().material = material;
    }

    //Created to place a roof tile.
    void PlaceRoofSprite(Vector2Int location, Vector2Int size)
    {
        Vector3 fixedLoaction = SpriteFloorLocationFix(size, location);
        Vector3 placeAt = new Vector3(fixedLoaction.x, fixedLoaction.y + 1.0f, fixedLoaction.z);

        GameObject go = Instantiate(GetRoofTile(fixedLoaction), placeAt, Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
        go.GetComponent<Transform>().rotation = Quaternion.Euler(90, 0, 0);
    }

    //Created to place a wall at given flags, with appropriate positions and rotation.
    void PlaceWallSprite(Vector2Int location, Vector2Int size, Material material, SpritePositionType relativePos)
    {
        SpritePositionType[] allWallTypes =
        {
            SpritePositionType.Top,
            SpritePositionType.Right,
            SpritePositionType.Bottom,
            SpritePositionType.Left
        };

        if ((relativePos | SpritePositionType.None) != SpritePositionType.None)
        {
            GameObject Wall = null;

            if ((relativePos & SpritePositionType.Top) == SpritePositionType.Top)
            {
                Wall = Instantiate(wallPrefab, SpriteWallLocationFix(size, location, (relativePos & SpritePositionType.Top)), Quaternion.identity);
                Wall.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Wall != null)
            {
                Wall.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
                Wall.GetComponent<SpriteRenderer>().material = material;
            }

            Wall = null;

            if ((relativePos & SpritePositionType.Bottom) == SpritePositionType.Bottom)
            {
                Wall = Instantiate(wallPrefab, SpriteWallLocationFix(size, location, (relativePos & SpritePositionType.Bottom)), Quaternion.identity);
                Wall.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Wall != null)
            {
                Wall.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
                Wall.GetComponent<SpriteRenderer>().material = material;
            }

            Wall = null;

            if ((relativePos & SpritePositionType.Left) == SpritePositionType.Left)
            {
                Wall = Instantiate(wallPrefab, SpriteWallLocationFix(size, location, (relativePos & SpritePositionType.Left)), Quaternion.identity);
                Wall.GetComponent<Transform>().rotation = Quaternion.Euler(0, 270, 0);
            }
            if (Wall != null)
            {
                Wall.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
                Wall.GetComponent<SpriteRenderer>().material = material;
            }

            Wall = null;

            if ((relativePos & SpritePositionType.Right) == SpritePositionType.Right)
            {
                Wall = Instantiate(wallPrefab, SpriteWallLocationFix(size, location, (relativePos & SpritePositionType.Right)), Quaternion.identity);
                Wall.GetComponent<Transform>().rotation = Quaternion.Euler(0, 90, 0);
            }
            if (Wall != null)
            {
                Wall.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 1);
                Wall.GetComponent<SpriteRenderer>().material = material;
            }
        }
    }

    //Modified to place a sprite on each unit of a room.
    void PlaceRoom(Vector2Int location, Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                SpritePositionType spriteRelativePosition = GetSpriteRelativePosition(i, j, size);

                Vector2Int nextSpriteLocation = new Vector2Int(location.x + i, location.y + j);
                PlaceFloorSprite(nextSpriteLocation, new Vector2Int(1, 1), redMaterial);
                PlaceWallSprite(nextSpriteLocation, new Vector2Int(1, 1), greenMaterial, spriteRelativePosition);
            }
        }
    }

    //Method to calculate a sprites position, to understand where walls should be placed.
    SpritePositionType GetSpriteRelativePosition(int xPos, int yPos, Vector2Int size)
    {
        SpritePositionType position = SpritePositionType.None;

        if (xPos == 0)
        {
            position = position | SpritePositionType.Left;
        }
        if (xPos == (size.x - 1))
        {
            position = position | SpritePositionType.Right;
        }
        if (yPos == 0)
        {
            position = position | SpritePositionType.Bottom;
        }
        if (yPos == (size.y - 1))
        {
            position = position | SpritePositionType.Top;
        }

        return position;
    }

    void PlaceHallway(Vector2Int location)
    {
        Vector3 floorDetection = SpriteFloorLocationFix(new Vector2Int(1, 1), location);
        floorDetection = new Vector3(floorDetection.x, floorDetection.y - 0.1f, floorDetection.z);

        if (!DetectSprite(floorDetection))
        {
            /*
         * Before creating hallways, the program must find out what walls to delete, and learn the hallways relative
         * position, based on the deleted walls.
         */
            Vector3 wallDetection = SpriteFloorLocationFix(new Vector2Int(1, 1), location);
            wallDetection = new Vector3(wallDetection.x, wallDetection.y + 0.7f, wallDetection.z);
            Collider[] wallsFound = Physics.OverlapSphere(wallDetection, 0.5f);

            SpritePositionType hallwayWalls = (
                SpritePositionType.Top |
                SpritePositionType.Bottom |
                SpritePositionType.Left |
                SpritePositionType.Right);

            foreach (Collider wall in wallsFound)
            {
                float wallXPos = wall.gameObject.transform.position.x;
                float wallZPos = wall.gameObject.transform.position.z;

                if (wallXPos > wallDetection.x)
                {
                    hallwayWalls = hallwayWalls & ~SpritePositionType.Right;
                }
                if (wallXPos < wallDetection.x)
                {
                    hallwayWalls = hallwayWalls & ~SpritePositionType.Left;
                }
                if (wallZPos > wallDetection.z)
                {
                    hallwayWalls = hallwayWalls & ~SpritePositionType.Top;
                }
                if (wallZPos < wallDetection.z)
                {
                    hallwayWalls = hallwayWalls & ~SpritePositionType.Bottom;
                }

                Destroy(wall.gameObject);
            }

            PlaceFloorSprite(location, new Vector2Int(1, 1), blueMaterial);
            PlaceWallSprite(location, new Vector2Int(1, 1), greenMaterial, hallwayWalls);
        }

        
    }

    //Method created to place sprites in the correct location, since sprite position is based on centre.
    Vector3 SpriteFloorLocationFix(Vector2Int spriteSize, Vector2Int spriteLocation)
    {
        float spriteLocationX = spriteLocation.x + (spriteSize.x / 2.0f);
        float spriteLocationY = spriteLocation.y + (spriteSize.y / 2.0f);

        return new Vector3(spriteLocationX, 0, spriteLocationY);
    }

    Vector3 SpriteWallLocationFix(Vector2Int spriteSize, Vector2Int spriteLocation, SpritePositionType relativePos)
    {
        float spriteLocationX = spriteLocation.x + (spriteSize.x / 2.0f);
        float spriteLocationZ = spriteLocation.y + (spriteSize.y / 2.0f);

        if (relativePos == SpritePositionType.Left)
        {
            spriteLocationX = spriteLocation.x;
        }
        else if (relativePos == SpritePositionType.Right)
        {
            spriteLocationX = spriteLocation.x + spriteSize.x;
        }

        if (relativePos == SpritePositionType.Top)
        {
            spriteLocationZ = spriteLocation.y + spriteSize.x;
        }
        else if (relativePos == SpritePositionType.Bottom)
        {
            spriteLocationZ = spriteLocation.y;
        }

        return new Vector3(spriteLocationX, 0.5f, spriteLocationZ);
    }

    //Method to choose a randomly selected sprite from the available prefabs.
    GameObject NextSprite()
    {
        return spritePrefabs[random.Next(0, spritePrefabs.Length)];
    }

    //Used to detect floor tiles.
    bool DetectSprite (Vector3 atPosition)
    {
        bool existsAtPosition = false;

        Collider[] foundColliders = Physics.OverlapSphere(atPosition, 0.1f);

        if (foundColliders.Length > 0)
        {
            existsAtPosition = true;
        }

        return existsAtPosition;
    }

    //Used to place roof tiles.
    void PlaceRoof()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector2Int location = new Vector2Int(i, j);

                Vector3 floorDetectionRange = SpriteFloorLocationFix(new Vector2Int(1, 1), location);
                floorDetectionRange = new Vector3(floorDetectionRange.x, floorDetectionRange.y - 0.1f, floorDetectionRange.z);

                if (!DetectSprite(floorDetectionRange))
                {
                    PlaceRoofSprite(location, new Vector2Int(1, 1));
                }
            }
        }
    }

    //Used to get the required roof tile, based on the walls closest to the roof tile.
    GameObject GetRoofTile(Vector3 location)
    {
        SpritePositionType wallPositions = SpritePositionType.None;

        Vector3 wallDetector = new Vector3(location.x, location.y + 0.7f, location.z);
        Collider[] wallsFound = Physics.OverlapSphere(wallDetector, 0.5f);

        int arrayLocation = 0;

        foreach (Collider wall in wallsFound)
        {
            float wallXPos = wall.gameObject.transform.position.x;
            float wallZPos = wall.gameObject.transform.position.z;

            if (wallXPos > wallDetector.x)
            {
                wallPositions = wallPositions | SpritePositionType.Right;
            }
            if (wallXPos < wallDetector.x)
            {
                wallPositions = wallPositions | SpritePositionType.Left;
            }
            if (wallZPos > wallDetector.z)
            {
                wallPositions = wallPositions | SpritePositionType.Top;
            }
            if (wallZPos < wallDetector.z)
            {
                wallPositions = wallPositions | SpritePositionType.Bottom;
            }
        }

        switch (wallPositions)
        {
            case SpritePositionType.Top:
                arrayLocation = 1;
                break;
            case SpritePositionType.Bottom:
                arrayLocation = 2;
                break;
            case SpritePositionType.Left:
                arrayLocation = 3;
                break;
            case SpritePositionType.Right:
                arrayLocation = 4;
                break;
            case SpritePositionType.Top | SpritePositionType.Left:
                arrayLocation = 5;
                break;
            case SpritePositionType.Top | SpritePositionType.Right:
                arrayLocation = 6;
                break;
            case SpritePositionType.Bottom | SpritePositionType.Left:
                arrayLocation = 7;
                break;
            case SpritePositionType.Bottom | SpritePositionType.Right:
                arrayLocation = 8;
                break;
            default:
                arrayLocation = GetCornerRoofTiles(wallDetector);
                break;
        }

        return roofPrefabs[arrayLocation];
    }

    int GetCornerRoofTiles(Vector3 originalDetector)
    {
        SpritePositionType wallPositions = SpritePositionType.None;

        Vector3 extendedDetector = new Vector3(
            originalDetector.x,
            originalDetector.y,
            originalDetector.z);
        Collider[] wallsFound = Physics.OverlapSphere(extendedDetector, 1.0f);

        int arrayLocation = 0;

        foreach (Collider wall in wallsFound)
        {
            float wallXPos = wall.gameObject.transform.position.x;
            float wallZPos = wall.gameObject.transform.position.z;

            if (wallXPos > extendedDetector.x)
            {
                wallPositions = wallPositions | SpritePositionType.Right;
            }
            if (wallXPos < extendedDetector.x)
            {
                wallPositions = wallPositions | SpritePositionType.Left;
            }
            if (wallZPos > extendedDetector.z)
            {
                wallPositions = wallPositions | SpritePositionType.Top;
            }
            if (wallZPos < extendedDetector.z)
            {
                wallPositions = wallPositions | SpritePositionType.Bottom;
            }
        }

        switch (wallPositions)
        {
            case SpritePositionType.Top | SpritePositionType.Left:
                arrayLocation = 9;
                break;
            case SpritePositionType.Top | SpritePositionType.Right:
                arrayLocation = 10;
                break;
            case SpritePositionType.Bottom | SpritePositionType.Left:
                arrayLocation = 11;
                break;
            case SpritePositionType.Bottom | SpritePositionType.Right:
                arrayLocation = 12;
                break;
            default:
                arrayLocation = 0;
                break;
        }

        return arrayLocation;
    }
}
