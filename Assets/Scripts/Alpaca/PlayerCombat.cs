using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: MatthewCopeland
public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    [Header("Melee Attack")]
    public MeleeWeapon melee;
    public float meleeAttackRate = 2f;
    

    [Header("Ranged Attack")]
    public RangedWeapon ranged;
    public float shootAttackRate = 2f;

    private float nextAttackTime = 0f;

    void Update()
    {        
        //If attack has reset to avoid attack spamming
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Attack");
                melee.Attack();
                nextAttackTime = Time.time + 1f / meleeAttackRate;
            } else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ranged.Attack();
                nextAttackTime = Time.time + 1f / shootAttackRate;
            }
        }
    }

}
