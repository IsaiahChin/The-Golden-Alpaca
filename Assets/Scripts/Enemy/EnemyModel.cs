using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    // MVC
    private EnemyView view;
    public Transform AttackPoint { get; set; }
    public Transform Target { get; set; }
    public NavMeshAgent Agent { get; set; }

    //Melee attack stats
    public float MeleeAttackRate { get; set; }
    public float MeleeAttackRange { get; set; }
    public bool MeleeEnabled { get; set; }

    //Ranged attack stats
    public float RangedAttackRate { get; set; }
    public float RangedAttackRange { get; set; }

    public bool RangedEnabled { get; set; }

    //General settings
    public LayerMask attackLayer;
    public float NextAttackTime { get; set; }

    public float SightRange { get; set; }
    public LayerMask followLayer;

    public float Health { get; set; }

    public float WanderRadius { get; set; }
    public float WanderTimer { get; set; }
    public float Timer { get; set; }


    void Start()
    {
        view = GetComponent<EnemyView>();

        MeleeAttackRate = 0.5f;
        MeleeAttackRange = 1.0f;

        RangedAttackRate = 0.5f;
        RangedAttackRange = 5.0f;

        NextAttackTime = 0.0f;

        SightRange = 6;

        Target = GameObject.Find("Alpaca").transform;
        Agent = GetComponent<NavMeshAgent>();
        Health = 1;

        WanderTimer = UnityEngine.Random.Range(4, 10);
        WanderRadius = SightRange * 2;

        Timer = WanderTimer;

        AttackPoint = this.gameObject.transform.GetChild(1).transform;
    }

    public void ChasePlayer()
    {
        if (Health > 0)
        {
            Agent.SetDestination(Target.position);
        }
    }

    public void IdleMove()
    {
        if (Health > 0)
        {
            Agent.SetDestination(RandomNavmeshLocation(WanderRadius));
            Timer = 0;
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
