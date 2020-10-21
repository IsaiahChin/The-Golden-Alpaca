using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public bool isGameOver;
    public GameObject gameOverMenu;

    void Start()
    {
        DisableGameOver();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    /**
     * This method pauses the time scale of the game and sets a boolean variable
     * to true. Then activates a Game Over menu screen.
     */
    public void EnableGameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        gameOverMenu.SetActive(isGameOver);
    }

    /**
     * This method resets the time scale of the game and sets a boolean variable
     * to false. Then deactivates the Game Over menu screen.
     */
    public void DisableGameOver()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        gameOverMenu.SetActive(isGameOver);
    }
}
