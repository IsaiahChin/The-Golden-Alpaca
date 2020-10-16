using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite openedDoor;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        sr.sprite = openedDoor;
    }
}
