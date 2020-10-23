using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script destroys the game object that it is applied to after a certain amount of time
 */
public class DestroySelf : MonoBehaviour
{
    public float destroyCountdown;

    void Start()
    {
        Destroy(gameObject, destroyCountdown);
    }
}
