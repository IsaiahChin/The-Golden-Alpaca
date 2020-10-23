using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public GameObject MainMenuUI;
    public Slider volumeSlider;
    void Start(){
        FindObjectOfType<AudioManager>().PlayBGM("Theme");
        volumeSlider.value = PlayerPrefs.GetFloat("volume",0f);
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
