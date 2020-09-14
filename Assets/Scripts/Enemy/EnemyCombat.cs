using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    //Melee attack stats
    public MeleeWeapon melee;
    public float meleeAttackRate = 2f;

    bool playerInMeleeRange;
    public LayerMask player;

    float nextAttackTime = 0f;

    void Update()
    {
        playerInMeleeRange = Physics.CheckSphere(melee.attackPoint.position, melee.attackRange, player);

        //If attack has reset to avoid attack spamming
        if (Time.time >= nextAttackTime)
        {
            if (playerInMeleeRange)
            {
                //Melee attack
                //animator.SetTrigger("Attack");
                melee.Attack();
                nextAttackTime = Time.time + 1f / meleeAttackRate;
            }
        }
    }

}
