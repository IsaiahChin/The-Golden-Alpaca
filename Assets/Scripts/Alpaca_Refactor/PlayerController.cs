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
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();

        meleeWeaponScript = GetComponent<MeleeWeapon>();
        rangedWeaponScript = GetComponent<RangedWeapon>();

        pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>();
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverController>();
    }

    void Update()
    {
        if (pauseMenu.GameIsPaused == false)
        {
            model.newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            // Check if player should be dead
            if (model.health <= 0.0f)
            {
                playerHealthScript.UpdateHealth();
                playerHealthScript.InitiateGameOver();
                view.SetDead(true);
                gameOverMenu.EnableGameOver();
                model.rigidBody.velocity = new Vector3(0, 0, 0); // Stop player movement
                model.enabled = false;
                this.enabled = false;
            }

            // Health testing
            if (Input.GetKeyDown(KeyCode.RightBracket)) // Increase health by one half
            {
                HealPlayer(0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.LeftBracket)) // Decrease health by one half
            {
                DamagePlayer(0.5f);
            }

            if (Time.time >= model.nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    view.swordAnimator.SetTrigger("Attack");
                    meleeWeaponScript.Attack();
                    model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    rangedWeaponScript.Attack();
                    model.nextAttackTime = Time.time + 1f / model.rangedAttackRate;
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
        //view.animator.SetTrigger("Hit");
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
}
