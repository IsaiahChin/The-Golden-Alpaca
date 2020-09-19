using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 newPosition;
    [SerializeField]
    private Sprite deadSprite;

    // Scripts
    private PlayerMovement movement;            // Holds new input direction from user to move to.
    private PlayerHealthUI playerHealthScript;  // Links health UI to player.

    [SerializeField]
    private float health, maxSpeed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Initialize speed
        maxSpeed = 5.0f;
        movement = new PlayerMovement(maxSpeed);

        // Initialize health
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI>();
        health = playerHealthScript.maxHealth;
    }

    private void Update()
    {
        // Get player input for new direction, and store it.
        newPosition = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));

        if (newPosition.x != 0.0f || newPosition.z != 0.0f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        health = playerHealthScript.currentHealth;
        if (health <= 0.0f)
        {
            spriteRenderer.sprite = deadSprite;
            newPosition = new Vector3(0, 0, 0);
            animator.enabled = false; // Stop animator from playing
            this.enabled = false; // Remove player controls
        }
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player.
        rigidBody.velocity = movement.CalculateMovement(newPosition);
    }
}
