using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//Author: MatthewCopeland
public class MeleeWeapon : MonoBehaviour
{
    //Target Layers
    public LayerMask targetLayers;
    public Transform attackPoint { get; set; }
    
    public float attackRange { get; set; }
    public float AttackDamage { get; set; }

    private void Start()
    {
        attackRange = 1f;
        AttackDamage = 0.5f;
        attackPoint = this.gameObject.transform.GetChild(1).transform;
    }

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
                enemy.GetComponent<PlayerController>().takeDamage(AttackDamage);
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
        if (attackPoint != null)
        {
            //Draw sphere from the attack point of the size of the attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
