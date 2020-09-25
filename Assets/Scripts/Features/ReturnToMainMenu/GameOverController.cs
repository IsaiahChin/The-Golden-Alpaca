using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public bool isGameOver;
    public GameObject gameOverMenu;

    void Start()
    {
        DisableGameOver();
    }

    public void EnableGameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        gameOverMenu.SetActive(isGameOver);
    }

    public void DisableGameOver()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        gameOverMenu.SetActive(isGameOver);
    }
}
