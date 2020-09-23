using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float health;

    private void Update()
    {
        if (health<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
        Debug.Log("Hit with " + damage + " points of damage, current health is " + health);
    }
}
