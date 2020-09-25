using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Author: MatthewCopeland
public class EnemyCombat : MonoBehaviour
{
    private Animator animator;

    //Melee attack stats
    [Header("Melee Attack")]
    public MeleeWeapon melee;
    public float meleeAttackRate = 1f;
    public float meleeAttackRange = 1f;

    //Ranged attack stats
    [Header("Ranged Attack")]
    public RangedWeapon ranged;
    public float rangedAttackRate = 1f;
    public float rangedAttackRange = 5f;

    //Attack range states
    private bool playerInMeleeRange;
    private bool playerInRangedRange;

    //General settings
    [Header("Settings")]
    public LayerMask attackLayer;
    public bool showSceneLabels;

    //Attack cooldown
    private float nextAttackTime = 0f;

    /**
     * This method finds the sword animatior
     */
    private void Start()
    {
        animator = GameObject.Find("EnemySword").GetComponent<Animator>();
    }

    /**
     * This method controls manages the enemies attack
     */
    void FixedUpdate()
    {
        //Check what range the player is from the enemy
        playerInMeleeRange = Physics.CheckSphere(
            melee.attackPoint.position,
            meleeAttackRange,
            attackLayer);
        playerInRangedRange = Physics.CheckSphere(ranged.attackPoint.position, rangedAttackRange, attackLayer);

        //If attack has reset to avoid attack spamming
        if (Time.time >= nextAttackTime)
        {
            if (playerInMeleeRange)
            {
                //If the player is in the melee attack range then preform a melee attack
                animator.SetTrigger("Attack");
                melee.Attack();
                nextAttackTime = Time.time + 1f / meleeAttackRate;
            }
            else if(playerInRangedRange)
            {
                //If the player is in the ranged attack range then preform a ranged attack
                ranged.Attack();
                nextAttackTime = Time.time + 1f / rangedAttackRate;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //If there is no attack point, do nothing
        if (showSceneLabels&&melee.attackPoint!=null&&ranged.attackPoint!=null)
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
