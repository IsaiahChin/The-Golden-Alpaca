using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshBaker : MonoBehaviour
{
    public NavMeshSurface surface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bake();
        }
    }

    public void Bake()
    {
        Debug.Log("Bake");
        surface.BuildNavMesh();
    }

    
}
