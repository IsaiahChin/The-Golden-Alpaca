using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    
    void Start(){
        //moves slider to previous value
        volumeSlider.value = PlayerPrefs.GetFloat("volume",0f);
    }
    //Adjust master volume with slider on options menu
    public void SetVoulume(float volume){
        audioMixer.SetFloat("volume",volume);
        //Saves user preference so slider won't reset to 0 
        PlayerPrefs.SetFloat("volume",volume);
    }
}
