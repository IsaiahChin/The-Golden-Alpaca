using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerPosition;
    public NavMeshAgent agent;

    public float sightRange;
    bool playerInSightRange;
    public LayerMask player;

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);

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
        agent.SetDestination(playerPosition.position);
    }

    private void OnDrawGizmosSelected()
    {
        //If there is no view point, do nothing
        if (GetComponent<Transform>() == null)
        {
            return;
        }
        else
        {
            //Draw sphere from the view point of the size of the view range
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GetComponent<Transform>().position, sightRange);
        }

    }
}
