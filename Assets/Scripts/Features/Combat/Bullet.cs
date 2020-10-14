using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

//Author: MatthewCopeland
public class Bullet : MonoBehaviour
{
    public Animator animator;

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
    * This method self destructs the bullet if it has been flying for too long
    */
    private void FixedUpdate()
    {
        timer += 1.0f * Time.deltaTime;
        if (timer >= decayTime)
        {
            Destroy(gameObject);
        }
    }

    /**
    * This method damages gameobjects based on their tag when a bullet collides with it
    */
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && collision.tag != shooterTag)
        {
            //If the collision is with a player and the player didnt shoot it, then self destrict
            collision.GetComponent<PlayerController>().DamagePlayer(damage);
            Hit();
        }
        else if (collision.tag == "Enemy" && collision.tag != shooterTag)
        {
            //If the collision is with a enemy and the enemy didnt shoot it, then self destrict
            collision.GetComponent<EnemyHealthController>().decreaseHealth(damage);
            Hit();
        }
        else if (collision.tag == "Enviroment" && collision.tag != shooterTag)
        {
            //If the collision is with the enviroment, self destrict
            Hit();
        }
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Wait for animation to play before destroying
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
