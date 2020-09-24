﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // MVC
    private PlayerModel model;
    private PlayerView view;

    private PlayerHealthUI_Refactor playerHealthScript; // Links health UI to player
    private MeleeWeapon melee;
    private RangedWeapon ranged;

    void Start()
    {
        melee = this.gameObject.GetComponent<MeleeWeapon>();
        ranged = this.gameObject.GetComponent<RangedWeapon>();
        model = gameObject.AddComponent<PlayerModel>();
        view = gameObject.AddComponent<PlayerView>();
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();
    }

    void Update()
    {
        model.newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Check if player should be dead
        if (model.health <= 0.0f)
        {
            playerHealthScript.UpdateHealth();
            view.animator.SetBool("isDead", true);
            model.rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement
            Destroy(model);
            this.enabled = false;
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

        if (Time.time >= model.nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                view.swordAnimator.SetTrigger("Attack");
                melee.Attack();
                model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ranged.Attack();
                model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
            }
        }

    }

    /**
     * This method increases the health attribute by the life parameter
     */
    public void heal(float life)
    {
        model.health += life;
        if (model.health > model.maxHealth)
        {
            model.health = model.maxHealth;
        }
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