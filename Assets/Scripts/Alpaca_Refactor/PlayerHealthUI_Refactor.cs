using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerHealthUI_Refactor : MonoBehaviour
{
    [Header("Health Stats")]
    public float maxHealth;
    public float currentHealth;

    [Header("Heart Assets")]
    public GameObject canvas;
    public GameObject heartStorage;
    [Space]
    public GameObject emptyHeart;
    public GameObject halfHeart;
    public GameObject fullHeart;
    public GameObject lastHalfHeart;

    [Header("Game Over Assets")]
    public GameObject gameOverText;

    [Header("Scripts")]
    public PlayerController player;

    private void Start()
    {
        canvas = transform.parent.gameObject;
        StartCoroutine(LateStart());
    }

    /**
     * This method is called after the Start method with a 1 second delay.
     * This is to ensure that other scripts with variables are instantiated first.
     */
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        currentHealth = player.getHealth();
        maxHealth = player.getMaxHealth();
        UpdateHealth();
    }

    /**
     * This method refreshes the UI hearts depending on the amount of health the player has.
     */
    public void UpdateHealth()
    {
        currentHealth = player.getHealth();
        maxHealth = player.getMaxHealth();
        if (currentHealth > maxHealth) // Check if health goes over max health
        {
            currentHealth = maxHealth; // Don't allow health overflow
        }
        else
        {
            foreach (Transform heart in transform) // Remove all heart prefabs from the Heart Storage
            {
                Destroy(heart.gameObject);
            }

            for (int i = 0; i < maxHealth; i++) // Instantiate heart prefabs
            {
                if (currentHealth == i + 1)
                {
                    Instantiate(fullHeart, heartStorage.transform);
                }
                else if (currentHealth > i)
                {
                    if (currentHealth < (i + 1))
                    {
                        if (currentHealth == 0.5f)
                        {
                            Instantiate(lastHalfHeart, heartStorage.transform);
                        }
                        else
                        {
                            Instantiate(halfHeart, heartStorage.transform);
                        }
                    }
                    else
                    {
                        Instantiate(fullHeart, heartStorage.transform);
                    }
                }
                else
                {
                    Instantiate(emptyHeart, heartStorage.transform);
                }
            }

            if (currentHealth <= 0.0f) //Check if player is dead
            {
                currentHealth = 0.0f;
                // Create Game Over Text in parent object (Script is attached to Heart Storage object, child of Canvas)
                Instantiate(gameOverText, transform.parent.gameObject.transform);
                this.enabled = false; // Disable this script
            }
        }
    }
}
