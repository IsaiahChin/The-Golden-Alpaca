using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
    private bool mediationAccomplished = false;

    void Update()
    {
        PlayerHealthUI_Refactor healthInfo = GameObject.Find("HealthCanvas").GetComponentInChildren<PlayerHealthUI_Refactor>();
        GameObject player = GameObject.Find("Alpaca");

        if ((healthInfo != null) && (player != null))
        {
            bool healthStartHasRun = healthInfo.startHasRun;

            if (healthStartHasRun)
            {
                EventHandeler.ActivateHealthUI();
                mediationAccomplished = true;
            }
        }

        if (mediationAccomplished)
        {
            Destroy(this);
        }
    }
}
