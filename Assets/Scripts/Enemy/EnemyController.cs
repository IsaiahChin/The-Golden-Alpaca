using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // MVC
    private EnemyModel model;
    private EnemyView view;

    public MeleeWeapon melee;
    public RangedWeapon ranged;

    private void Start()
    {
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();

        melee = GetComponent<MeleeWeapon>();
        ranged = GetComponent<RangedWeapon>();
    }

    private void Update()
    {
        //Check if the player is in the sight range
        model.playerInSightRange = Physics.CheckSphere(transform.position, model.sightRange, model.followLayer);

        model.playerInMeleeRange = Physics.CheckSphere(melee.attackPoint.position, model.meleeAttackRange, model.attackLayer);
        //Debug.Log("Melee" + model.playerInMeleeRange);
        model.playerInRangedRange = Physics.CheckSphere(ranged.attackPoint.position, model.rangedAttackRange, model.attackLayer);
        //Debug.Log("Ranged" + model.playerInRangedRange);

        if (model.playerInSightRange)
        {
            Debug.Log("Chasing" + model.playerInSightRange);
            model.ChasePlayer();
        }
        else
        {
            //Sprint 2 TODO: Idle movement would go here
        }

        if (Time.time >= model.nextAttackTime)
        {
            if (model.playerInMeleeRange)
            {
                //If the player is in the melee attack range then preform a melee attack
                //view.animator.SetTrigger("Attack");
                melee.Attack();
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
            else if (model.playerInRangedRange)
            {
                //If the player is in the ranged attack range then preform a ranged attack
                ranged.Attack();
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }
        }

    }
}