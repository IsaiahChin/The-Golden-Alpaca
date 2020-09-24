using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Player : MonoBehaviour
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
        animator = GetComponent<Animator>();
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI>();

        // Initialize speed
        maxSpeed = 4.0f;
        movement = new PlayerMovement(maxSpeed);

        // Initialize health
        maxHealth = 3.0f;
        health = maxHealth;
        playerHealthScript.UpdateHealth();
    }

    private void Update()
    {
        // Get player input for new direction, and store it
        newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        UpdateAnimator(CalculateMousePosition());

        // Update health + check for dead player
        if (health <= 0.0f)
        {
            playerHealthScript.UpdateHealth();
            rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement
            animator.SetBool("isDead", true);
            this.enabled = false; // Remove player controls
        }

        // Health testing
        if (Input.GetKeyDown(KeyCode.RightBracket)) // Increase health by one half
        {
            increaseHealth(0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket)) // Decrease health by one half
        {
            decreaseHealth(0.5f);
        }
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player
        rigidBody.velocity = movement.CalculateMovement(newPosition);
    }

    /**
     * This method increases the health attribute by the life parameter
     */
    private void increaseHealth(float life)
    {
        health += life;
        playerHealthScript.UpdateHealth();
    }

    /**
     * This method decreases the health attribute by the damage parameter
     */
    public void decreaseHealth(float damage)
    {
        health -= damage;
        playerHealthScript.UpdateHealth();
    }

    /**
     * This method calculates and returns the mouse cursor position relative to in-game
     */
    private Vector3 CalculateMousePosition()
    {
        Vector3 mousePos = -Vector3.one;
        Plane mousePlane = new Plane(Vector3.up, 0.0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        if (mousePlane.Raycast(ray, out distanceToPlane))
        {
            mousePos = ray.GetPoint(distanceToPlane);
        }

        return mousePos;
    }

    /**
     * This method updates the player animator based on movement and mouse position
     */
    private void UpdateAnimator(Vector3 mousePos)
    {
        // Change booleans in player animator depending on movement speed
        if (newPosition.x != 0.0f || newPosition.z != 0.0f) // Check if player is moving
        {
            animator.SetBool("isMoving", true);
            /**
             * Note: No check for x == 0.0f because we want to retain 
             * the previous "Right" or "Left" state when moving only up or down
            **/
            if (newPosition.x > 0) // Check if moving to the right
            {
                if (mousePos.x > transform.position.x) // Check if mouse is right of player
                {
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                    animator.SetFloat("Playback Speed", 1.0f);
                }
                else
                {
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    animator.SetFloat("Playback Speed", -1.0f);
                }
            }
            else
            {
                if (mousePos.x > transform.position.x)
                {
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                    animator.SetFloat("Playback Speed", -1.0f);
                }
                else
                {
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    animator.SetFloat("Playback Speed", 1.0f);
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetFloat("Playback Speed", 1.0f);
        }
    }
}
