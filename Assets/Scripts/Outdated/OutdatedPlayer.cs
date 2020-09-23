using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdatedPlayer : MonoBehaviour
{
    // Components
    private Rigidbody rigidBody;
    private Animator animator;
    private Vector3 newPosition;

    // Scripts
    private PlayerMovement movement;            // Holds new input direction from user to move to
    private PlayerHealthUI playerHealthScript;  // Links health UI to player

    // Player attributes
    public float health { get; set; }
    public float maxHealth { get; set; }
    [SerializeField]
    private float maxSpeed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        movement = new PlayerMovement(maxSpeed);
    }

    private void Update()
    {
        // Get player input for new direction, and store it
        newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player
        rigidBody.velocity = movement.CalculateMovement(newPosition);
    }
}
