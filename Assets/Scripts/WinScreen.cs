using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void Start()
    {
        //Play Music
        FindObjectOfType<AudioManager>().PlayBGM("YouWinScene");
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        // Wait two seconds otherwise player may immdiately move on the next scene
        yield return new WaitForSeconds(2);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }

    }
}
