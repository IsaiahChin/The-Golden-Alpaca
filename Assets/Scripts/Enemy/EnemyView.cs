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

}
