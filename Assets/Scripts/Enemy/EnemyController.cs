using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 100f;
    public float currentHealth;
    public Light myLight;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyCombat>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        myLight.enabled = false;
        this.enabled = false;
    }
}
