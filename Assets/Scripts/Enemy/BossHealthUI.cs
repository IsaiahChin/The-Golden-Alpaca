using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthUI : MonoBehaviour
{
    [Header("Health Stats")]
    public float maxHealth;
    public float currentHealth;

    [Header("Heart Assets")]
    public GameObject canvas;
    [Space]
    public GameObject fullLeftBar;
    public GameObject fullBar;
    public GameObject fullRightBar;
    [Space]
    public GameObject emptyLeftBar;
    public GameObject emptyBar;
    public GameObject emptyRightBar;

    [Header("Script")]
    public EnemyController enemy;
    public bool isHealthBarActive { get; set; }

    void Start()
    {
        canvas = transform.parent.gameObject;
        isHealthBarActive = false;
        StartCoroutine(LateStart(1f));
    }

    IEnumerator LateStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (SceneManager.GetActiveScene().name.Equals("Level Three"))
        {
            enemy = GameObject.Find("BossSunflower(Clone)").GetComponent<EnemyController>();
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level Five"))
        {
            enemy = GameObject.Find("BossRoboBall(Clone)").GetComponent<EnemyController>();
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        currentHealth = enemy.GetHealth();
        maxHealth = enemy.GetMaxHealth();
        foreach (Transform healthBar in transform) // Remove all health bar prefabs from the Health Storage
        {
            Destroy(healthBar.gameObject);
        }

        for (float i = 0.0f; i <= maxHealth; i += 0.5f) // Instantiate health bar prefabs
        {
            if (i == 0.0f) // Left End of health bar
            {
                if (currentHealth == i)
                {
                    Instantiate(emptyLeftBar, transform);
                }
                else
                {
                    Instantiate(fullLeftBar, transform);
                }
            }
            else
            {
                if (i == maxHealth) // Right End of health bar
                {
                    if (currentHealth == i)
                    {
                        Instantiate(fullRightBar, transform);
                    }
                    else
                    {
                        Instantiate(emptyRightBar, transform);
                    }
                }
                else // Health bars inbetween
                {
                    if (i < currentHealth)
                    {
                        Instantiate(fullBar, transform);
                    }
                    else
                    {
                        Instantiate(emptyBar, transform);
                    }
                }
            }
        }
        if (!isHealthBarActive)
        {
            canvas.SetActive(false);
        }
    }
    public void ShowHealthBar()
    {
        canvas.SetActive(true);
        isHealthBarActive = true;
    }
}
