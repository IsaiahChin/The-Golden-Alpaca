using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Animator swordAnimator { get; set; }
    public Animator animator { get; set; }

    private void Start()
    {
        swordAnimator = GameObject.Find("EnemySword").GetComponent<Animator>();
    }

}
