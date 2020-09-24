using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is to be used only for the Death Feature Scene.
 * It handles the alpaca 
 */
public class PlayerDeathFeature : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Sprite deadSprite;

    // Attributes
    public float health;
    private GameObject heartStorage;
    private DeathFeature deathFeatureScript;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        heartStorage = GameObject.Find("Heart Storage");
        deathFeatureScript = heartStorage.GetComponent<DeathFeature>();
        health = deathFeatureScript.maxHealth;
    }

    void Update()
    {
        // Constantly update health attribute with health value from UI script
        health = deathFeatureScript.currentHealth;

        // Check if player should be dead
        if (health <= 0.0f)
        {
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        }
    }
}
