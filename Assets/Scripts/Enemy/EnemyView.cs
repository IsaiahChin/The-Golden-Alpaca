using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Animator animator { get; set; }

    private void Start()
    {
        animator = GameObject.Find("EnemySword").GetComponent<Animator>();
    }

}
