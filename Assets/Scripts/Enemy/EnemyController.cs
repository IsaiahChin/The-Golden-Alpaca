using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // MVC
    private EnemyModel model;
    private EnemyView view;

    //Weapons
    private MeleeWeapon melee;
    private RangedWeapon ranged;

    private void Start()
    {
        //MVC linking
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();

        if (model.meleeEnabled == true)
        {
            melee = gameObject.GetComponent<MeleeWeapon>();
            melee.AttackDamage = model.meleeAttackDamage;
            melee.attackRange = model.meleeAttackRange;
        }

        if (model.rangedEnabled == true)
        {
            ranged = gameObject.GetComponent<RangedWeapon>();
            ranged.damage = model.rangedAttackDamage;
            ranged.speed = model.rangedAttackProjectileSpeed;
        }

        //Increase the enemy counter
        GameObject.Find("CounterCanvas").GetComponentInChildren<EnemyCounter>().increaseCount();
    }

    private void Update()
    {
        //Check Health
        if (model.health <= 0)
        {
            Die();
        }

        if (model.movementEnabled)
        {
            CalculateMovement();
        }

        //If the enemy can attack and has line of sight, then attack
        if (Time.time >= model.NextAttackTime && PlayerInLineOfSight() && (model.meleeEnabled||model.rangedEnabled))
        {
            CalculateAttack();
        }
    }

    /**
    * This method moves the player either to chase or idle 
    */
    private void CalculateMovement()
    {
        if (PlayerInLineOfSight())
        {
            model.ChasePlayer();
        }
        else
        {
            model.Timer += Time.deltaTime;
            if (model.Timer >= model.WanderTimer)
            {
                model.IdleMove();
            }
        }
    }

    /**
    * This method calculates if the enemy has line of sight to the player
    */
    private bool PlayerInLineOfSight()
    {
        if (Physics.Raycast(model.AttackPoint.position, model.AttackPoint.forward, out RaycastHit hit, model.sightRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /**
    * This method calculates an enemy attack based on their assigned weapon structure
    */
    private void CalculateAttack()
    {
        //If the enemy has a melee and ranged attack
        if (model.meleeEnabled && model.rangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.targetLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.meleeAttackRate;
            } //Check if the player is within range
            else if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.targetLayer))
            {
                ranged.Attack(model.rangedAttackPattern.ToString());
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }

        } //If the enemy has a melee but no ranged attack
        else if (model.meleeEnabled && !model.rangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.targetLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
        } //If the enemy has a ranged but no melee attack
        else if (model.rangedEnabled && !model.meleeEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.targetLayer))
            {
                ranged.Attack(model.rangedAttackPattern.ToString());
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }
        }

    }

    private void Die()
    {
        //Decrease enemy counter
        GameObject.Find("CounterCanvas").GetComponentInChildren<EnemyCounter>().decreaseCount();

        //Deactivate MVC
        model.enabled = false;
        view.enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }

    public void decreaseHealth(float damage)
    {
        model.health -= damage;
        //view.animator.SetTrigger("Hit");
    }
}