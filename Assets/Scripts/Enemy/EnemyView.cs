using System;
using UnityEngine;
using UnityEngine.Video;

public class EnemyView : MonoBehaviour
{
    public Animator SwordAnimator { get; set; }
    public Animator animator { get; set; }

    void Start()
    {
        //Gets the animator component from the GFX object
        animator = this.gameObject.transform.GetChild(transform.childCount-1).GetComponent<Animator>();
    }

    public void InitiateSword()
    {
        //Gets the animator component from the Sword GFX object
        SwordAnimator = this.gameObject.transform.GetChild(transform.childCount - 2).GetComponent<Animator>();
    }

    public void SetMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }

    public void SetDirection(bool right, bool left)
    {
        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
    }
}
