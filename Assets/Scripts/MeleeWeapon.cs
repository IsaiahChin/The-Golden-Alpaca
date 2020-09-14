using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MeleeWeapon : MonoBehaviour
{
    //Attack Point
    public Transform attackPoint;
    
    //ENemy Layers
    public LayerMask enemyLayers;

    //Attack Stats
    public float attackRange = 0.5f;
    public float meleeAttackDamage = 40f;

    //Standard melee attack function
    public void Attack()
	{
        //Get all the coliders within the attack range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Damage each collider with an enemy layer 
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyController>()!=null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(meleeAttackDamage);
            }
            else
            {
                enemy.GetComponent<PlayerController>().TakeDamage(meleeAttackDamage);
            }

            
        }
    }

    //Draws an representation of the attack range on the attack point for the scene view
    private void OnDrawGizmosSelected()
    {
        //If there is no attack point, do nothing
        if (attackPoint == null)
        {
            return;
        }
        else
        {
            //Draw sphere from the attack point of the size of the attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }
}
