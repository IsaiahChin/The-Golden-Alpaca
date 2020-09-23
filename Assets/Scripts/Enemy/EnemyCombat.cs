using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Author: MatthewCopeland
public class EnemyCombat : MonoBehaviour
{
    private Animator animator;

    [Header("Melee Attack")]
    public MeleeWeapon melee;
    public float meleeAttackRate = 2f;
    public float meleeAttackRange = 5f;

    [Header("Ranged Attack")]
    public RangedWeapon ranged;
    public float rangedAttackRate = 2f;
    public float rangedAttackRange = 10f;

    private bool playerInMeleeRange;
    private bool playerInRangedRange;

    [Header("Settings")]
    public LayerMask attackLayer;
    public bool showSceneLabels;

    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GameObject.Find("EnemySword").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerInMeleeRange = Physics.CheckSphere(melee.attackPoint.position, meleeAttackRange, attackLayer);
        playerInRangedRange = Physics.CheckSphere(ranged.attackPoint.position, rangedAttackRange, attackLayer);

        //If attack has reset to avoid attack spamming
        if (Time.time >= nextAttackTime)
        {
            if (playerInMeleeRange)
            {
                //Melee attack
                animator.SetTrigger("Attack");
                melee.Attack();
                nextAttackTime = Time.time + 1f / meleeAttackRate;
            }
            else if(playerInRangedRange)
            {
                ranged.Attack();
                nextAttackTime = Time.time + 1f / rangedAttackRate;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //If there is no attack point, do nothing
        if (showSceneLabels)
        {
            //Draw sphere from the attack point of the size of the attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(melee.attackPoint.position, meleeAttackRange);
            Gizmos.DrawWireSphere(ranged.attackPoint.position, rangedAttackRange);
            Handles.Label(new Vector3(melee.attackPoint.position.x, melee.attackPoint.position.y - meleeAttackRange, melee.attackPoint.position.z), "Melee Range: "+meleeAttackRange);
            Handles.Label(new Vector3(ranged.attackPoint.position.x, ranged.attackPoint.position.y - rangedAttackRange, ranged.attackPoint.position.z), "Ranged Range: "+rangedAttackRange);
        }
    }
}
