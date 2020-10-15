using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshBaker : MonoBehaviour
{
    public NavMeshSurface surface;

    private void Start()
    {
        surface.BuildNavMesh();
    }
}
