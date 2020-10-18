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

    [Header("Scripts")]
    public PlayerController player;

    [Header("Detection")]
    private bool alpacaExists;

    private void Start()
    {
        canvas = transform.parent.gameObject;
        alpacaExists = false;
        EventHandeler.OnPlayerSpawn += getPlayerInfo;
    }

    /**
     * This method refreshes the UI hearts depending on the amount of health the player has.
     */
    public void UpdateHealth()
    {
        if (alpacaExists)
        {
            currentHealth = player.GetHealth();
            maxHealth = player.GetMaxHealth();
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
            }
        }
    }

    /**
     * This method instantiates game over text in the UI and disables script
     */
    public void InitiateGameOver()
    {
        currentHealth = 0.0f;
        this.enabled = false; // Disable this script
    }

    /**
     * Method to get player information required for script to run.
     */
    private void getPlayerInfo()
    {
        player = GameObject.Find("Alpaca").GetComponent<PlayerController>();
        currentHealth = player.GetHealth();
        maxHealth = player.GetMaxHealth();
        UpdateHealth();
        alpacaExists = true;
    }

    /**
     * Remove method refernce from event handeler to remove chance of memory leak.
     */
    private void OnDestroy()
    {
        EventHandeler.OnPlayerSpawn -= getPlayerInfo;
    }
}
