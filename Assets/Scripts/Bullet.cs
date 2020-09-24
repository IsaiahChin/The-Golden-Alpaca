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

    private void Start()
    {
        decayTime = 2.0f;
        damage = 0.5f;
    }

    private void FixedUpdate()
    {
        timer += 1.0f * Time.deltaTime;
        if (timer>= decayTime)
        {
            Destroy(gameObject);
        }
    }

    //When a bullet collides with anything
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag=="Player"&&collision.tag!=shooterTag)
        {
            Debug.Log(shooterTag + " Hit " + collision.tag+" with "+damage+" damage - RANGED");
            collision.GetComponent<PlayerController>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy" && collision.tag != shooterTag)
        {
            Debug.Log(shooterTag + " Hit " + collision.tag + " with " + damage + " damage - RANGED");
            collision.GetComponent<EnemyHealthController>().decreaseHealth(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Enviroment" && collision.tag != shooterTag)
        {
            Destroy(gameObject);
        }       
    }
}
