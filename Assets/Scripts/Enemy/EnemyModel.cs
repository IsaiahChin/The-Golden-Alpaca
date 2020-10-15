using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    // MVC
    private EnemyView view;
    public Transform attackPoint { get; set; }
    public Transform target { get; set; }
    public NavMeshAgent agent { get; set; }

    //Melee attack stats
    public float meleeAttackRate { get; set; }
    public float meleeAttackRange { get; set; }
    public bool meleeEnabled { get; set; }

    //Ranged attack stats
    public float rangedAttackRate { get; set; }
    public float rangedAttackRange { get; set; }

    public bool rangedEnabled { get; set; }

    //General settings
    public LayerMask attackLayer;
    public float nextAttackTime { get; set; }

    public float sightRange { get; set; }
    public LayerMask followLayer;

    public float health { get; set; }

    public float wanderRadius { get; set; }
    public float wanderTimer { get; set; }
    public float timer { get; set; }


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
        agent = GetComponent<NavMeshAgent>();
        health = 1;

        wanderTimer = UnityEngine.Random.Range(4, 10);
        wanderRadius = sightRange * 2;

        timer = wanderTimer;

        attackPoint = this.gameObject.transform.GetChild(1).transform;
    }

    public void ChasePlayer()
    {
        if (health > 0)
        {
            agent.SetDestination(target.position);
        }
    }

    public void IdleMove()
    {
        if (health > 0)
        {
            agent.SetDestination(RandomNavmeshLocation(wanderRadius));
            timer = 0;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }


}
