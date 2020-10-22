using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject MainMenuUI;
    void Start(){
        FindObjectOfType<AudioManager>().PlayBGM("Theme");
    }
    void Update()
    {
        
        if (Input.anyKey)
        {
            MainMenuUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
}
