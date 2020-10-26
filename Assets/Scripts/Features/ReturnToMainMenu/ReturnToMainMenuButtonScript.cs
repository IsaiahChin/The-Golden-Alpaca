using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuButtonScript : MonoBehaviour
{
    /**
     * This method allows the player to return to the main menu scene
     */
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
