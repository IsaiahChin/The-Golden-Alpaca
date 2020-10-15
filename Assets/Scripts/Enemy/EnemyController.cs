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
            model.MeleeEnabled = false;
        }
        else
        {
            melee = gameObject.GetComponent<MeleeWeapon>();
            model.MeleeEnabled = true;
        }

        if (gameObject.GetComponent<RangedWeapon>() == null)
        {
            model.RangedEnabled = false;
        }
        else
        {
            ranged = gameObject.GetComponent<RangedWeapon>();
            model.RangedEnabled = true;
        }

        //Increase the enemy counter
        GameObject.Find("CounterCanvas").GetComponentInChildren<EnemyCounter>().increaseCount();
    }

    private void Update()
    {
        //Check Health
        if (model.Health <= 0)
        {
            Die();
        }

        CalculateMovement();

        //If the enemy can attack and has line of sight, then attack
        if (Time.time >= model.NextAttackTime && PlayerInLineOfSight())
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
        if (Physics.Raycast(model.AttackPoint.position, model.AttackPoint.forward, out RaycastHit hit, model.SightRange))
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
        if (model.MeleeEnabled && model.RangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.MeleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.MeleeAttackRate;
            } //Check if the player is within range
            else if (Physics.CheckSphere(ranged.attackPoint.position, model.RangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.RangedAttackRate;
            }

        } //If the enemy has a melee but no ranged attack
        else if (model.MeleeEnabled && !model.RangedEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(melee.attackPoint.position, model.MeleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.MeleeAttackRate;
            }
        } //If the enemy has a ranged but no melee attack
        else if (model.RangedEnabled && !model.MeleeEnabled)
        {
            //Check if the player is within range
            if (Physics.CheckSphere(ranged.attackPoint.position, model.RangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                //Reset the attack time
                model.NextAttackTime = Time.time + 1f / model.RangedAttackRate;
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
        model.Health -= damage;
        //view.animator.SetTrigger("Hit");
    }
}