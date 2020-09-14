using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet Damage
    public int damage=40;
	
    //When a bullet collides with anything
    private void OnTriggerEnter(Collider other)
    {
        //Check so that the bullet isnt collding with the player
        if (other.tag != "Player")
        {
            //Try get the enemy component
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                //If the collsision has an enemy comonent, then damage it
                enemy.TakeDamage(damage);
            }
            //Destroy the bullet
            Destroy(gameObject);
        }
	}	
}
