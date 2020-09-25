using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float health;
    public Animator animator;

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
        animator.SetTrigger("Hit");
    }
}
