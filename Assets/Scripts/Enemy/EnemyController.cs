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

        //Setup modular weapon system
        if (gameObject.GetComponent<MeleeWeapon>() == null)
        {
            model.meleeEnabled = false;
        }
        else
        {
            melee = gameObject.GetComponent<MeleeWeapon>();
            model.meleeEnabled = true;
        }

        if (gameObject.GetComponent<RangedWeapon>() == null)
        {
            model.rangedEnabled = false;
        }
        else
        {
            ranged = gameObject.GetComponent<RangedWeapon>();
            model.rangedEnabled = true;
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

        CalculateMovement();

        //If the enemy can attack and has line of sight, then attack
        if (Time.time >= model.nextAttackTime && PlayerInLineOfSight())
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
            model.timer += Time.deltaTime;
            if (model.timer >= model.wanderTimer)
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
        if (Physics.Raycast(model.attackPoint.position, model.attackPoint.forward, out RaycastHit hit, model.sightRange))
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
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            } //Check if the player is within range
            else if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                //Reset the attack time
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }

        } //If the enemy has a melee but no ranged attack
        else if (model.meleeEnabled && !model.rangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
        } //If the enemy has a ranged but no melee attack
        else if (model.rangedEnabled && !model.meleeEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                //Reset the attack time
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
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