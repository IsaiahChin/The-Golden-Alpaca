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
    private Color openColour;

    private Light doorLight;


    void Awake()
    {
        rendererS = gameObject.GetComponent<SpriteRenderer>();
        doorLight = GetComponentInChildren<Light>();
        doorLight.intensity = 3;
        EventHandeler.OnDoorInteraction += ToNextLevel;
    }

    public void OpenDoor()
    {
        if (!rendererS.sprite.name.Equals(openedDoor.name))
        {
            rendererS.sprite = openedDoor;
            doorLight.intensity = 7;
            doorLight.color = openColour;
        }
    }

    private void ToNextLevel()
    {
        if (rendererS.sprite.name.Equals(openedDoor.name))
        {
            int allScenes = SceneManager.sceneCountInBuildSettings;
            int sceneNo = SceneManager.GetActiveScene().buildIndex;
            int nextScene = -2;

            //Adjust for zero counting
            if (!sceneNo.Equals(allScenes - 1))
            {
                nextScene = sceneNo + 1;
            }
            else
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnDestroy()
    {
        EventHandeler.OnDoorInteraction -= ToNextLevel;
    }
}
