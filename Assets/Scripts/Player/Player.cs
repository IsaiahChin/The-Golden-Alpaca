using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /*
     * movement: Holds new input direction from user to move to.
     */
    private Rigidbody rb;
    private PlayerMovement movement;
    private Vector3 newPosition;

    [SerializeField]
    private float maxspeed = 10.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = new PlayerMovement(maxspeed);
    }

    private void Update()
    {
        // Get player input for new direction, and store it.
        newPosition = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player.
        rb.velocity = movement.CalculateMovement(newPosition);
    }
}
