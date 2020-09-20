using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;

    //Ranged attack stats
    public RangedWeapon ranged;
    public float shootAttackRate = 2f;

    //Melee attack stats
    public MeleeWeapon melee;
    public float meleeAttackRate = 2f;
    
    float nextAttackTime = 0f;
    public Animator animator;

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
           
                animator.SetTrigger("Attack");
                ranged.Attack();
                nextAttackTime = Time.time + 1f / shootAttackRate;
            }
        }
    }

}
