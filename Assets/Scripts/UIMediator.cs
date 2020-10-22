using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
    private bool uiMediationAccomplished = false;
    private bool exitMediationAccomplished = false;
    private bool hasRun = false;

    void Update()
    {
        UIMediation();
        ExitMediation();

        if (uiMediationAccomplished && exitMediationAccomplished && !hasRun)
        {
            //Run all events that should be activated on the player being spawned.
            hasRun = true;
            EventHandeler.ActivatePlayerSpawnEvent();
            Destroy(gameObject);
        }
    }

    /**
     * Detects if player has been placed, and the health canvis has been sucessfully activated.
     */
    private void UIMediation()
    {
        PlayerHealthUI_Refactor healthInfo = GameObject.Find("HealthCanvas").GetComponentInChildren<PlayerHealthUI_Refactor>();
        GameObject player = GameObject.Find("Alpaca");

        if ((healthInfo != null) && (player != null))
        {
            bool healthStartHasRun = healthInfo.startHasRun;

            if (healthStartHasRun)
            {
                uiMediationAccomplished = true;
            }
        }
    }

    /**
     * Detects if the procedural generation has finished placing the map.
     */
    private void ExitMediation()
    {
        GameObject procGenRef = GameObject.Find("TestGeneratorFirstFloor");

        if (procGenRef != null)
        {
            bool generationFinished = procGenRef.GetComponent<SpriteGenerator2D>().generationFinished;

            if (generationFinished)
            {
                exitMediationAccomplished = true;
            }
        }
    }
}
