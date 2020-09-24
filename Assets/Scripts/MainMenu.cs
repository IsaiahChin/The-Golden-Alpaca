using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //Start the game. Uses unity build to go to starting level
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //Exit the game
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
