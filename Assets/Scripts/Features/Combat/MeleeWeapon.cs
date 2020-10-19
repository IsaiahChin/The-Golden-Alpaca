using UnityEngine;

//Author: MatthewCopeland
public class MeleeWeapon : MonoBehaviour
{
    //Target Layers
    public LayerMask targetLayers;
    public Transform attackPoint { get; set; }
    [HideInInspector]
    public float attackRange=1f;
    [HideInInspector]
    public float AttackDamage=1f;

    private void Awake()
    {
        attackPoint = this.gameObject.transform.GetChild(1).transform;
    }

    /**
    * This method attacks anything within a given range
    */
    public void Attack()
	{
        //Get all the coliders within the attack range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayers);

        //Damage each collider with an enemy layer 
        foreach (Collider enemy in hitEnemies)
        {
            //Outdated: From combat development
            //Debug.Log(this.tag+" Hit " + enemy.name+" with "+AttackDamage+" damage - Melee");

            if (enemy.CompareTag("Player"))
            {
                //If the collider is a player, call the player damage script
                enemy.GetComponent<PlayerController>().DamagePlayer(AttackDamage);
            }
            else if (enemy.CompareTag("Enemy"))
            {
                //If the collider is a enemy, call the enemy damage script
                enemy.GetComponent<EnemyController>().decreaseHealth(AttackDamage);
            }
            else if (enemy.CompareTag("Bullet"))
            {
                Debug.Log("Hit bullet");
                //If the collider is a enemy, call the enemy damage script
                enemy.GetComponent<Bullet>().Hit();
            }
        }
    }

    /**
    * This method displays the melee attack range
    */
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
