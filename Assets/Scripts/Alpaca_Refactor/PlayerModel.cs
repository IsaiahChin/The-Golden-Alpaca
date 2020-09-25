using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    // MVC
    private PlayerView view;

    // Components
    public Rigidbody rigidBody { get; set; }
    public Vector3 newPosition { get; set; }

    // Player attributes
    public float health { get; set; }
    public float maxHealth { get; set; }
    public float maxSpeed { get; set; }

    // Attack attributes
    public float meleeAttackRate { get; set; }

    public float rangedAttackRate { get; set; }

    public float nextAttackTime { get; set; }

    void Start()
    {
        view = GetComponent<PlayerView>();
        rigidBody = GetComponent<Rigidbody>();
        
        // Initialize variables
        maxSpeed = 5.0f;
        maxHealth = 3.0f;
        health = maxHealth;

        meleeAttackRate = 2.0f;
        rangedAttackRate = 2.0f;
        nextAttackTime = 0.0f;
    }

    void Update()
    {
        // Update the player animation every frame based on the mouse position and movement
        UpdateAnimator(CalculateMousePosition());
    }

    private void FixedUpdate()
    {
        // Call method to add new vector to the speed of the player
        rigidBody.velocity = CalculateMovement(newPosition);
    }

    /**
     * This method calculates the speed in which the character is going in
     */
    public Vector3 CalculateMovement(Vector3 direction)
    {
        return direction * maxSpeed;
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
    public void UpdateAnimator(Vector3 mousePos)
    {
        // Change booleans in player animator depending on movement speed
        if (newPosition.x != 0.0f || newPosition.z != 0.0f) // Check if player is moving
        {
            view.SetMoving(true);
            /**
             * Note: No check for x == 0.0f because we want to retain 
             * the previous "Right" or "Left" state when moving only up or down
            **/
            if (newPosition.x > 0) // Check if moving to the right
            {
                if (mousePos.x > transform.position.x) // Check if mouse is right of player
                {
                    view.SetDirection(true, false);
                    view.SetPlaybackSpeed(1.0f);
                }
                else
                {
                    view.SetDirection(false, true);
                    view.SetPlaybackSpeed(-1.0f);
                }
            }
            else
            {
                if (mousePos.x > transform.position.x)
                {
                    view.SetDirection(true, false);
                    view.SetPlaybackSpeed(-1.0f);
                }
                else
                {
                    view.SetDirection(false, true);
                    view.SetPlaybackSpeed(1.0f);
                }
            }
        }
        else
        {
            view.SetMoving(false);
            view.SetPlaybackSpeed(1.0f);
        }
    }
}
