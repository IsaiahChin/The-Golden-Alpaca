using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject pauseMenuUI;

    public GameOverController gameOverController;

    private void Start()
    {
        //Set paused to false
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!gameOverController.isGameOver)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        //Resume the game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().ResetBGM();
    }

    private void Pause()
    {
        //Pause the game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioManager.nowPlaying.source.volume -= 0.3f;
        AudioManager.nowPlaying.source.pitch -= 0.1f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

