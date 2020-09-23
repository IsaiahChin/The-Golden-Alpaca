using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerView : MonoBehaviour
{
    //private PlayerController controller;

    public Animator animator { get; set; }

    void Start()
    {
        //controller = GetComponent<PlayerController>();

        animator = GetComponent<Animator>();
    }

    public void setMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }

    public void setPlaybackSpeed(float playbackSpeed)
    {
        animator.SetFloat("Playback Speed", playbackSpeed);
    }

    public void setDirection(bool right, bool left)
    {
        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
    }
}
