using System;
using UnityEngine;
using UnityEngine.Video;

public class EnemyView : MonoBehaviour
{
    public Animator SwordAnimator { get; set; }
    public Animator animator { get; set; }

    void Start()
    {
        animator = this.gameObject.transform.GetChild(transform.childCount-1).GetComponent<Animator>();
    }

    public void InitiateSword()
    {
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
