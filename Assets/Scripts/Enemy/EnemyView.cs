using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Animator SwordAnimator { get; set; }
    public Animator Animator { get; set; }

    void Start()
    {
        Animator = GameObject.Find("EnemyGFX").GetComponent<Animator>();
    }

    public void InitiateSword()
    {
        SwordAnimator = GameObject.Find("EnemySword").GetComponent<Animator>();
    }

    public void SetMoving(bool moving)
    {
        Debug.Log("Moving "+moving);
        Animator.SetBool("isMoving", moving);
    }

    public void SetDirection(bool right, bool left)
    {
        Debug.Log("Right " + right);
        Debug.Log("Left " + left);
        Animator.SetBool("Right", right);
        Animator.SetBool("Left", left);
    }
}
