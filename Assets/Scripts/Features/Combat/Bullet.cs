using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

//Author: MatthewCopeland
public class Bullet : MonoBehaviour
{
    private float timer;
    public string shooterTag { get; set; }
    public float decayTime { get; set; }
    public float damage { get; set; }

    /**
    * This method sets the damage and decay time of the bullet
    */
    private void Start()
    {
        decayTime = 2.0f;
        damage = 0.5f;
    }

    /**
    * This method self destructs teh bullet if it has been flying for too long
    */
    private void FixedUpdate()
    {
        timer += 1.0f * Time.deltaTime;
        if (timer>= decayTime)
        {
            Destroy(gameObject);
        }
    }

    /**
    * This method damages gameobjects based on their tag when a bullet collides with it
    */
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag=="Player"&&collision.tag!=shooterTag)
        {
            //If the collision is with a player and the player didnt shoot it, then self destrict
            Debug.Log(shooterTag + " Hit " + collision.tag+" with "+damage+" damage - RANGED");
            collision.GetComponent<PlayerController>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy" && collision.tag != shooterTag)
        {
            //If the collision is with a enemy and the enemy didnt shoot it, then self destrict
            Debug.Log(shooterTag + " Hit " + collision.tag + " with " + damage + " damage - RANGED");
            collision.GetComponent<EnemyHealthController>().decreaseHealth(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Enviroment" && collision.tag != shooterTag)
        {
            //If the collision is with the enviroment, self destrict
            Destroy(gameObject);
        }     
    }
}
