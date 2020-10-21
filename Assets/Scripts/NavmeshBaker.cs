using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshBaker : MonoBehaviour
{
    public NavMeshSurface surface;

    public void Bake()
    {
        surface.BuildNavMesh();
    }
}
