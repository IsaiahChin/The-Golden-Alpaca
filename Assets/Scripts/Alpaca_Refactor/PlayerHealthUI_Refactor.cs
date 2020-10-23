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

    [Header("Script")]
    public PlayerController player;

    private void Start()
    {
        canvas = transform.parent.gameObject;
        StartCoroutine(LateStart(0.5f));
        player = GameObject.Find("Alpaca").GetComponent<PlayerController>();
    }

    /**
     * This method is called after the Start method with a delay time.
     * This is to ensure that other scripts with variables are instantiated first.
     */
    IEnumerator LateStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        currentHealth = player.GetHealth();
        maxHealth = player.GetMaxHealth();
        UpdateHealth();
    }

    /**
     * This method refreshes the UI hearts depending on the amount of health the player has.
     */
    public void UpdateHealth()
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
                    Instantiate(fullHeart, transform);
                }
                else if (currentHealth > i)
                {
                    if (currentHealth < (i + 1))
                    {
                        if (currentHealth == 0.5f)
                        {
                            Instantiate(lastHalfHeart, transform);
                        }
                        else
                        {
                            Instantiate(halfHeart, transform);
                        }
                    }
                    else
                    {
                        Instantiate(fullHeart, transform);
                    }
                }
                else
                {
                    Instantiate(emptyHeart, transform);
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
}
