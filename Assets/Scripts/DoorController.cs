using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite openedDoor;

    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    //Test code for opening doors.
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!sr.sprite.name.Equals(openedDoor.name))
        {
            sr.sprite = openedDoor;
        }
    }
}
