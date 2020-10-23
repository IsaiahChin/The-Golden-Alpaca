using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // MVC
    private PlayerModel model;
    private PlayerView view;

    // Scripts
    private PlayerHealthUI_Refactor playerHealthScript; // Links health UI to player
    private MeleeWeapon meleeWeaponScript;              // Melee weapon script
    private RangedWeapon rangedWeaponScript;            // Ranged weapon script

    private PauseMenuController pauseMenu;              // Pause menu controller script
    private GameOverController gameOverMenu;            // Game over controller script

    void Start()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();

        meleeWeaponScript = GetComponent<MeleeWeapon>();
        meleeWeaponScript.AttackDamage = model.meleeAttackDamage;
        meleeWeaponScript.attackRange = model.meleeAttackRange;

        rangedWeaponScript = GetComponent<RangedWeapon>();
        rangedWeaponScript.damage = model.rangedAttackDamage;
        rangedWeaponScript.speed = model.rangedAttackProjectileSpeed;

        pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>();
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverController>();
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if ((pauseMenu != null) && (pauseMenu != null) && (pauseMenu != null))
        {
            if (pauseMenu.GameIsPaused == false && view.animator != null)
            {
                model.newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                CheckForDoors();

                // Check if player should be dead
                if (model.health <= 0.0f)
                {
                    playerHealthScript.UpdateHealth();
                    playerHealthScript.InitiateGameOver();
                    view.SetDead(true);
                    gameOverMenu.EnableGameOver();
                    model.rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement

                    // Disable MVC
                    model.enabled = false;
                    view.enabled = false;
                    this.enabled = false;
                }

                HealthPickup();

                if (Time.time >= model.nextAttackTime)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        view.swordAnimator.SetTrigger("Attack");
                        meleeWeaponScript.Attack();
                        model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
                    }
                    else if (Input.GetKey(KeyCode.Mouse1))
                    {
                        rangedWeaponScript.Attack(model.rangedAttackPattern.ToString());
                        model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
                    }
                }
            }
        }
        else
        {
            pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>();
            gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverController>();
            playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();
        }
    }

    /**
     * This method tries to detect and pickup health objects
     */
    private void HealthPickup()
    {
        if (model.health < model.maxHealth)
        {
            //Get all the coliders within the attack range
            Collider[] hits = Physics.OverlapSphere(this.transform.position, 0.3f, 1 << LayerMask.NameToLayer("Health"));

            //Damage each collider with an enemy layer 
            foreach (Collider health in hits)
            {
                if (health.CompareTag("Health"))
                {
                    //If the collider is a player, call the player damage script
                    HealPlayer(health.GetComponent<HealthPickupController>().Pickup());
                    health.GetComponent<HealthPickupController>().Destroy();
                }
            }
        }
    }

    /**
     * This method increases the health attribute by the life parameter
     */
    public void HealPlayer(float life)
    {
        model.health += life;
        if (model.health > model.maxHealth)
        {
            model.health = model.maxHealth;
        }
        playerHealthScript.UpdateHealth();
    }

    /**
     * This method decreases the health attribute by the damage parameter
     */
    public void DamagePlayer(float damage)
    {
        model.health -= damage;
        view.animator.SetTrigger("Hit");
        playerHealthScript.UpdateHealth();
    }

    public float GetHealth()
    {
        return model.health;
    }

    public float GetMaxHealth()
    {
        return model.maxHealth;
    }

    private void CheckForDoors()
    {
        Vector3 position = GetComponent<Transform>().position;
        Collider[] closeEnvironment = Physics.OverlapSphere(position, 0.5f, 1 << LayerMask.NameToLayer("Environment"));

        if (!closeEnvironment.Length.Equals(0))
        {
            Collider door = null;
            foreach (Collider possibleDoor in closeEnvironment)
            {
                if (possibleDoor.tag.Equals("Door"))
                {
                    door = possibleDoor;
                }
            }

            if (door != null)
            {
                EventHandeler.ActivateInteraction();
            }
        }
    }
}
