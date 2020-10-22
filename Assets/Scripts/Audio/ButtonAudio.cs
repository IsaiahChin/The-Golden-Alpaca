using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public void ClickSound(){
        FindObjectOfType<AudioManager>().Play("Button Click");
    }

    public void SelectSound(){
        FindObjectOfType<AudioManager>().Play("Button Select");
    }
}
