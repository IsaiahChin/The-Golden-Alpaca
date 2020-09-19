using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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
    private PlayerMovement movement;            // Holds new input direction from user to move to
    private PlayerHealthUI playerHealthScript;  // Links health UI to player

    // Player attributes
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
        // Get player input for new direction, and store it
        newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0.1f));


        // Change booleans in player animator depending on movement speed
        if (newPosition.x != 0.0f || newPosition.z != 0.0f) // Check if player is moving
        {
            animator.SetBool("isMoving", true);
            //Debug.Log("Mouse: " + mousePos);
            //Debug.Log("Alpaca: " + transform.position);

            /**
             * Note: No check for x == 0.0f because we want to retain 
             * the previous "Right" or "Left" state when moving only up or down
            **/
            if (newPosition.x > 0.0f) // Check if moving to the right
            //if (mousePos.x > transform.position.x) // Check if moving to the right
            {
                //TODO: Change on mouse x position
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
            }
            else if (newPosition.x < 0.0f) //Check if moving to the left
            //else if (mousePos.x < transform.position.x) //Check if moving to the left
            {
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Update health + check for dead player
        health = playerHealthScript.currentHealth;
        if (health <= 0.0f)
        {
            spriteRenderer.sprite = deadSprite;
            rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement
            animator.enabled = false; // Stop animator from playing
            this.enabled = false; // Remove player controls 
        }
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player
        rigidBody.velocity = movement.CalculateMovement(newPosition);
    }
}
