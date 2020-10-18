using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandeler : MonoBehaviour
{
    public delegate void DoorInteraction();
    public static event DoorInteraction OnDoorInteraction;

    public delegate void PlayerSpawn();
    public static event PlayerSpawn OnPlayerSpawn;

    public static void ActivateInteraction()
    {
        OnDoorInteraction?.Invoke();
    }

    public static void ActivatePlayerSpawn()
    {
        OnPlayerSpawn?.Invoke();
    }
}
