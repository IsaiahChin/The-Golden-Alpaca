using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is to be used only for the Death Feature Scene.
 * It handles the alpaca 
 */
public class PlayerDeathFeature : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Sprite deadSprite;
    public float health;

    private GameObject heartStorage;
    private DeathFeature deathFeatureScript;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        heartStorage = GameObject.Find("Heart Storage");
        deathFeatureScript = heartStorage.GetComponent<DeathFeature>();
        health = deathFeatureScript.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = deathFeatureScript.currentHealth;
        if (health <= 0.0f)
        {
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        }
    }
}
