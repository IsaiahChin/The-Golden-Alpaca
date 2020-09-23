using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//Author: MatthewCopeland
public class MeleeWeapon : MonoBehaviour
{
    [Header("Resources")]
    //Attack Point
    public Transform attackPoint;
    
    //Enemy Layers
    public LayerMask targetLayers;

    [Header("Attack Stats")]
    public float attackRange = 0.5f;
    public float AttackDamage = 1f;

    //Standard melee attack function
    public void Attack()
	{
        //Get all the coliders within the attack range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayers);

        //Damage each collider with an enemy layer 
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(this.tag+" Hit " + enemy.name+" with "+AttackDamage+" damage - Melee");

            if (enemy.tag == "Player")
            {
                enemy.GetComponent<Player>().decreaseHealth(AttackDamage);
            }
            else if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<EnemyHealthController>().decreaseHealth(AttackDamage);
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
