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
    //Enemy movement settings
    [Header("Settings")]
    public float sightRange;
    public LayerMask followLayer;
    public bool showSceneLabels;

    private bool playerInSightRange;
    private Transform target;
    private NavMeshAgent navmeshAgent;

    /**
     * This method finds the alpacas location and the navmesh agent
     */
    private void Start()
    {
        target = GameObject.Find("Alpaca").transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    /**
     * This method manages the movement states
     */
    private void Update()
    {
        //CHeck if the player is in the sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, followLayer);

        if (playerInSightRange)
        {
            ChasePlayer();
        }
        else
        {
            //Sprint 2 TODO: Idle movement would go here
        }

    }

    /**
     * This method moves the agent towards the targets position
     */
    private void ChasePlayer()
    {
        navmeshAgent.SetDestination(target.position);
    }

    /**
     * This method displays the sight range and targeting in the scene view
     */
    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(GetComponent<Transform>().position, sightRange);
            Handles.Label(new Vector3(transform.position.x, transform.position.y - sightRange, transform.position.z), "Sight Range: "+sightRange);
        }
    }
}
