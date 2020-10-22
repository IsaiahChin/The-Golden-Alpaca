using System.Collections;
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

        //Setup melee
        if (model.meleeEnabled == true)
        {
            melee = gameObject.GetComponent<MeleeWeapon>();
            melee.AttackDamage = model.meleeAttackDamage;
            melee.attackRange = model.meleeAttackRange;
        }

        //Setup ranged
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
        if (model!=null&&view!=null)
        {
            //Check Health
            if (model.health <= 0)
            {
                Die();
            }

            //Move enemy
            if (model.movementEnabled)
            {
                CalculateMovement();
            }

            //If the enemy can attack and has line of sight and has a weapon, then attack
            if (Time.time >= model.NextAttackTime && PlayerInLineOfSight() && (model.meleeEnabled||model.rangedEnabled))
            {
                CalculateAttack();
            }
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
        //Raycast between the player and the enemy
        if (Physics.Raycast(model.AttackPoint.position, model.AttackPoint.forward, out RaycastHit hit, model.sightRange))
        {
            //If the raycast hits the player then it has line of sight
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
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
                PerformMeleeAttack();
            } //Check if the player is within range
            else if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.targetLayer))
            {
                PerformRangedAttack();
            }
        } //If the enemy has a melee but no ranged attack
        else if (model.meleeEnabled && !model.rangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.targetLayer))
            {
                PerformMeleeAttack();
            }
        } //If the enemy has a ranged but no melee attack
        else if (model.rangedEnabled && !model.meleeEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.targetLayer))
            {
                PerformRangedAttack();
            }
        }
    }
    private void PerformMeleeAttack()
    {
        //Reset the attack time
        model.NextAttackTime = Time.time + 1f / model.meleeAttackRate;

        StartCoroutine(MeleeAttackDelay(model.meleeAttackDelay));
    }
    private void PerformRangedAttack()
    {
        //Reset the attack time
        model.NextAttackTime = Time.time + 1f / model.rangedAttackRate;

        StartCoroutine(RangedAttackDelay(model.rangedAttackDelay));
    }

    IEnumerator MeleeAttackDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        melee.Attack();
        view.animator.SetTrigger("Attack");
        FindObjectOfType<AudioManager>().Play("Enemy Melee");
        
    }
    IEnumerator RangedAttackDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ranged.Attack(model.rangedAttackPattern.ToString());
        view.animator.SetTrigger("Attack");
        FindObjectOfType<AudioManager>().Play("Enemy Melee");
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("Enemy Dead");//Create death cloud particle effect
        
        Instantiate(model.deathCloudObject, transform.position, new Quaternion(0, 0, 0, 0));

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
        FindObjectOfType<AudioManager>().Play("Enemy Hit");
        model.health -= damage;
        view.animator.SetTrigger("Hit");
    }
}