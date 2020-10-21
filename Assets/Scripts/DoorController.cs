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
        GetComponentInChildren<Light>().intensity = 3;
        EventHandeler.OnDoorInteraction += ToNextLevel;
    }

    public void OpenDoor()
    {
        if (!rendererS.sprite.name.Equals(openedDoor.name))
        {
            rendererS.sprite = openedDoor;
            GetComponentInChildren<Light>().intensity = 7;
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
