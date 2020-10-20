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
    public float meleeAttackDelay;
    [Min(0.1f)]
    public float meleeAttackRange;
    [Min(0.1f)]
    public float meleeAttackDamage;

    [Header("Ranged Attack")]
    public bool rangedEnabled = true;
    [Min(0.1f)]
    public float rangedAttackRate;
    [Min(0.1f)]
    public float rangedAttackDelay;
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
        //MVC setup
        view = GetComponent<EnemyView>();
        
        if (swordGFXEnabled==true)
        {
            view.InitiateSword();
        }

        //Navigation setup
        Target = GameObject.Find("Alpaca").transform;
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.speed = speed;
        NavAgent.acceleration = acceleration;

        //Idle movement
        WanderTimer = UnityEngine.Random.Range(4, 10);
        Timer = WanderTimer;
        idleSearchRadius = sightRange * 2;

        //Velocity for animation setup
        PrevPos = transform.position;
        NewPos = transform.position;

        //Attack setup
        AttackPoint = this.gameObject.transform.GetChild(1).transform;
        NextAttackTime = 0.0f;
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        //Sets the object velocity based on position at each fixed update
        NewPos = transform.position;
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;
        PrevPos = NewPos;
    }

    /**
    * This method moves the enemy towards the player
    */
    public void ChasePlayer()
    {
        //Health check
        if (health > 0)
        {
            NavAgent.SetDestination(Target.position);
        }
    }

    /**
    * This method moves the enemy towards a random position inside the search radius
    */
    public void IdleMove()
    {
        //Health check
        if (health > 0)
        {
            NavAgent.SetDestination(RandomNavmeshLocation(idleSearchRadius));
            Timer = 0;
        }
    }

    /**
    * This method returns a random position near the enemy on the navmesh
    */
    public Vector3 RandomNavmeshLocation(float radius)
    {
        //Get random direction
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        //Intersect the random direction with the navmesh
        NavMeshHit navmeshHit;
        Vector3 navmeshPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out navmeshHit, radius, 1))
        {
            navmeshPosition = navmeshHit.position;
        }
        return navmeshPosition;
    }

    /**
    * This method updates the animator
    */
    public void UpdateAnimator()
    {
        // Change booleans in enemy animator depending on movement speed
        if (ObjVelocity.magnitude > 0) // Check if enemy is moving
        {
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
