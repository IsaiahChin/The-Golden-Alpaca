using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    // MVC
    private EnemyView view;

    //Melee attack stats
    public float meleeAttackRate { get; set; }
    public float meleeAttackRange { get; set; }

    //Ranged attack stats
    public float rangedAttackRate { get; set; }
    public float rangedAttackRange { get; set; }

    //Attack range states
    public bool playerInRangedRange { get; set; }
    public bool playerInMeleeRange { get; set; }


    //General settings
    public LayerMask attackLayer;
    public float nextAttackTime { get; set; }

    public float sightRange { get; set; }
    public LayerMask followLayer;

    public float health { get; set; }

    public void ChasePlayer()
    {
        if (health>0)
        {
            navmeshAgent.SetDestination(target.position);
        }        
    }

    public bool playerInSightRange { get; set; }
    public Transform target { get; set; }
    public NavMeshAgent navmeshAgent { get; set; }

    void Start()
    {
        view = GetComponent<EnemyView>();
       
        meleeAttackRate = 0.5f;
        meleeAttackRange = 1.0f;

        rangedAttackRate = 0.5f;
        rangedAttackRange = 5.0f;

        nextAttackTime = 0.0f;

        sightRange = 6;

        target = GameObject.Find("Alpaca").transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        health = 1;

    }
}
