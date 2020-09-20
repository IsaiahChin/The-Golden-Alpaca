using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

//Author: MatthewCopeland
public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public int damage=40;
    public float decayTime = 2;
    private float timer;
    public string shooterTag;

    private void Update()
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
            Debug.Log(shooterTag + " Hit " + collision.tag+" with "+damage+" damage");
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy" && collision.tag != shooterTag)
        {
            Debug.Log(shooterTag + " Hit " + collision.tag + " with " + damage + " damage");
            Destroy(gameObject);
        }
        else if (collision.tag == "Enviroment" && collision.tag != shooterTag)
        {
            Destroy(gameObject);
        }       
    }
}
