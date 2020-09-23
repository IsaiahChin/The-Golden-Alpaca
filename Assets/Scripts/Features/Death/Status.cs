using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class is to be used only for the Death Feature Scene.
 * It handles UI text for the status of the player
 */
public class Status : MonoBehaviour
{
    public GameObject status;
    public Text statusText;

    // Start is called before the first frame update
    void Start()
    {
        statusText.text = "Alive";
    }

    public void UpdateText(string newText)
    {
        statusText.text = newText;
    }
}
