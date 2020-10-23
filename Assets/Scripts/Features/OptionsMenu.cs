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
        volumeSlider.value = PlayerPrefs.GetFloat("volume",0f);
    }
    //Adjust master volume with slider on options menu
    public void SetVoulume(float volume){
        //Master mixer scales up logarithmic and not linearly unlike volume slider 
        audioMixer.SetFloat("volume",volume);
        PlayerPrefs.SetFloat("volume",volume);
    }
}
