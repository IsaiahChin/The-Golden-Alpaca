using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour
{
    public Animator animator { get; set; }
    public Animator swordAnimator { get; set; }

    void Start()
    {
        string level = SceneManager.GetActiveScene().name;
        FindObjectOfType<AudioManager>().PlayBGM(level);
        animator = GameObject.Find("AlpacaGFX").GetComponent<Animator>();
        swordAnimator = GameObject.Find("AlpacaSword").GetComponent<Animator>();
    }
    public void SetDead(bool dead)
    {
        animator.SetBool("isDead", dead);
    }
    public void SetMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }

    public void SetPlaybackSpeed(float playbackSpeed)
    {
        animator.SetFloat("Playback Speed", playbackSpeed);
    }

    public void SetDirection(bool right, bool left)
    {
        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
    }
}
