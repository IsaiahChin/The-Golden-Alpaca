using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
    private bool uiMediationAccomplished = false;
    private bool exitMediationAccomplished = false;

    void Update()
    {
        UIMediation();
        ExitMediation();

        if (uiMediationAccomplished && exitMediationAccomplished)
        {
            EventHandeler.ActivatePlayerSpawnEvent();
            Destroy(gameObject);
        }
    }

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
