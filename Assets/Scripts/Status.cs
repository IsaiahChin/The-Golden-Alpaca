using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
