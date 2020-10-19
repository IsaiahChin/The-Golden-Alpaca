using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    // MVC
    private EnemyView view;
    public Transform AttackPoint { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float NextAttackTime { get; set; }
    public NavMeshAgent NavAgent { get; set; }
    public float idleSearchRadius { get; set; }
    public Transform Target { get; set; }
    public float WanderTimer { get; set; }
    public float Timer { get; set; }

    public Vector3 PrevPos;
    public Vector3 NewPos;
    public Vector3 ObjVelocity;

    [Header("Settings")]
    [Min(0.5f)]
    public float health;
    [Min(0.1f)]
    public float speed;
    [Min(0.1f)]
    public float acceleration;
    [Min(0.5f)]
    public float sightRange;
    public bool movementEnabled = true;
    
    public LayerMask targetLayer;
    public LayerMask followLayer;

    [Header("Melee Attack")]
    public bool swordGFXEnabled = true;
    public bool meleeEnabled=true;
    [Min(0.1f)]
    public float meleeAttackRate;
    [Min(0.1f)]
    public float meleeAttackRange;
    [Min(0.1f)]
    public float meleeAttackDamage;

    [Header("Ranged Attack")]
    public bool rangedEnabled = true;
    [Min(0.1f)]
    public float rangedAttackRate;
    [Min(0.1f)]
    public float rangedAttackRange;
    [Min(0.1f)]
    public float rangedAttackDamage;
    [Min(0.1f)]
    public float rangedAttackProjectileSpeed;

    public enum AttackPattern { Default, Shotgun, CardinalPoints }
    public AttackPattern rangedAttackPattern;

    void Start()
    {
        PrevPos = transform.position;
        NewPos = transform.position;
        view = GetComponent<EnemyView>();
        Rigidbody = GetComponent<Rigidbody>();
        if (swordGFXEnabled==true)
        {
            view.InitiateSword();
        }
        Target = GameObject.Find("Alpaca").transform;
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.speed = speed;
        NavAgent.acceleration = acceleration;
        AttackPoint = this.gameObject.transform.GetChild(1).transform;
        NextAttackTime = 0.0f;
        WanderTimer = UnityEngine.Random.Range(4, 10);
        Timer = WanderTimer;
        idleSearchRadius = sightRange * 2;
    }
    private void Update()
    {
        UpdateAnimator();
    }
    private void FixedUpdate()
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
    }

    public void ChasePlayer()
    {
        if (health > 0)
        {
            NavAgent.SetDestination(Target.position);
        }
    }

    public void IdleMove()
    {
        if (health > 0)
        {
            NavAgent.SetDestination(RandomNavmeshLocation(idleSearchRadius));
            Timer = 0;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit navmeshHit;
        Vector3 navmeshPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out navmeshHit, radius, 1))
        {
            navmeshPosition = navmeshHit.position;
        }
        return navmeshPosition;
    }

    public void UpdateAnimator()
    {
        // Change booleans in player animator depending on movement speed
        if (ObjVelocity.magnitude > 0) // Check if player is moving
        {
            //Debug.Log("Moving "+ObjVelocity);
            view.SetMoving(true);
            /**
             * Note: No check for x == 0.0f because we want to retain 
             * the previous "Right" or "Left" state when moving only up or down
            **/
            if (ObjVelocity.x > 0) // Check if moving to the right
            {
                view.SetDirection(true, false);
            }
            else
            {
                view.SetDirection(false, true);
            }
        }
        else
        {
            view.SetMoving(false);
        }
    }
}
