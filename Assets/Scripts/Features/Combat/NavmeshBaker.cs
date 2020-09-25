using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class NavmeshBaker : MonoBehaviour
{
    //public NavMeshSurface
    void Start()
    {
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }
}
