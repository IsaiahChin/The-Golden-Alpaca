using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//Author: MatthewCopeland
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent navmeshAgent;

    [Header("Settings")]
    public float sightRange;
    public LayerMask followLayer;
    public bool showSceneLabels;

    private bool playerInSightRange;

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, followLayer);

        if (playerInSightRange)
        {
            ChasePlayer();
        }
        else
        {
            
        }

    }

    private void ChasePlayer()
    {
        navmeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels)
        {
            //Draw sphere from the view point of the size of the view range
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(GetComponent<Transform>().position, sightRange);
            Handles.Label(new Vector3(transform.position.x, transform.position.y - sightRange, transform.position.z), "Sight Range: "+sightRange);
        }
    }
}
