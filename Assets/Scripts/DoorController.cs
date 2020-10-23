using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer rendererS;
    [SerializeField]
    private Sprite openedDoor;

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
            int allScenes = SceneManager.sceneCountInBuildSettings;
            int sceneNo = SceneManager.GetActiveScene().buildIndex;
            int nextScene;

            //Adjust for zero counting
            if (!sceneNo.Equals(allScenes - 1))
            {
                nextScene = sceneNo + 1;
            }
            else
            {
                nextScene = 0;
            }

            GameObject.Find("Alpaca").GetComponent<Transform>().position = -Vector3.one;

            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnDestroy()
    {
        EventHandeler.OnDoorInteraction -= ToNextLevel;
    }
}
