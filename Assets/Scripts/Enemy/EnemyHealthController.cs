using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float health;
    public Animator animator;

    [SerializeField]
    private GameObject deathCloudObject;

    private void Update()
    {
        if (health<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathCloudObject, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
    }
}
