using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private float maxspeed { get; set; }

    public PlayerMovement(float maxspeed)
    {
        this.maxspeed = maxspeed;
    }

    // Calculates the speed in which the character is going in.
    public Vector3 CalculateMovement(Vector3 direction)
    {
        return direction * maxspeed;
    }
}
