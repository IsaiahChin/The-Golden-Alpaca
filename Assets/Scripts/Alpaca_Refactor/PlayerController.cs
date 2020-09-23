﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;

    private PlayerHealthUI_Refactor playerHealthScript; // Links health UI to player

    void Start()
    {
        model = gameObject.AddComponent<PlayerModel>();
        view = gameObject.AddComponent<PlayerView>();
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();
        playerHealthScript.UpdateHealth();
    }

    void Update()
    {
        model.newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Check if player should be dead
        if (model.health <= 0.0f)
        {
            playerHealthScript.UpdateHealth();
            model.rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement
            view.animator.SetBool("isDead", true);
            this.enabled = false; // Remove player controls
        }

        // Health testing
        if (Input.GetKeyDown(KeyCode.RightBracket)) // Increase health by one half
        {
            heal(0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket)) // Decrease health by one half
        {
            takeDamage(0.5f);
        }
    }

    /**
     * This method increases the health attribute by the life parameter
     */
    public void heal(float life)
    {
        model.health += life;
        playerHealthScript.UpdateHealth();
    }

    /**
     * This method decreases the health attribute by the damage parameter
     */
    public void takeDamage(float damage)
    {
        model.health -= damage;
        playerHealthScript.UpdateHealth();
    }

    public float getHealth()
    {
        return model.health;
    }

    public float getMaxHealth()
    {
        return model.maxHealth;
    }
}
