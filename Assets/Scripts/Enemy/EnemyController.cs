using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // MVC
    private EnemyModel model;
    private EnemyView view;

    private MeleeWeapon melee;
    private RangedWeapon ranged;

    private void Start()
    {
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();

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
        if (model.health <= 0)
        {
            Die();
        }
        
        CalculateMovement();

        if (Time.time >= model.nextAttackTime&& playerInLineOfSight())
        {
            CalculateAttack();
        }
    }

    private void CalculateMovement()
    {
        if (playerInLineOfSight())
        {
            model.ChasePlayer();
        }
        else
        {
            //Sprint 2 TODO: Idle movement would go here
        }
    }

    private bool playerInLineOfSight()
    {
        RaycastHit hit;

        if (Physics.Raycast(model.attackPoint.position, model.attackPoint.forward, out hit, model.sightRange))
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

    private void CalculateAttack()
    {
        if (model.meleeEnabled && model.rangedEnabled)
        {
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
            else if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }

        }
        else if (model.meleeEnabled && !model.rangedEnabled)
        {
            if (Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.attackLayer))
            {
                melee.Attack();
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
        }
        else if (model.rangedEnabled && !model.meleeEnabled)
        {
            
            if (Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.attackLayer))
            {
                ranged.Attack();
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }
        }
        
    }

    private void Die()
    {
        //Decrease enemy counter
        GameObject.Find("CounterCanvas").GetComponentInChildren<EnemyCounter>().decreaseCount();
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