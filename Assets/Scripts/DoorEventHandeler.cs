﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventHandeler : MonoBehaviour
{
    public delegate void DoorInteraction();
    public static event DoorInteraction OnDoorInteraction;

    public static void ActivateInteraction()
    {
        OnDoorInteraction();
    }
}