using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer rendererS;
    [SerializeField]
    private Sprite openedDoor;
    [SerializeField]
    private string nextLevelName;

    void Awake()
    {
        rendererS = gameObject.GetComponent<SpriteRenderer>();
        EventHandeler.OnDoorInteraction += ToNextLevel;
    }

    public void OpenDoor()
    {
        if (!rendererS.sprite.name.Equals(openedDoor.name))
        {
            rendererS.sprite = openedDoor;
        }
    }

    private void ToNextLevel()
    {
        if (rendererS.sprite.name.Equals(openedDoor.name))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void OnDestroy()
    {
        EventHandeler.OnDoorInteraction -= ToNextLevel;
    }
}
