using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class NavmeshBaker : MonoBehaviour
{
    //public navmeshsurface

    // Start is called before the first frame update
    void Start()
    {
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh(); 
    }
}
