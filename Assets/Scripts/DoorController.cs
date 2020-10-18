using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite openedDoor;
    [SerializeField]
    private string nextLevelName;

    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        DoorEventHandeler.OnDoorInteraction += ToNextLevel;
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

    private void ToNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
        DoorEventHandeler.OnDoorInteraction -= ToNextLevel;
    }
}
