using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        GetComponent<PlayerCombat>().enabled = false;
        myLight.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
    }
}
