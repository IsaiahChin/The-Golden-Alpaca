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

                 // Pause menu controller script
    private GameOverController gameOverMenu;            // Game over controller script

    void Start()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
        playerHealthScript = GameObject.Find("Heart Storage").GetComponent<PlayerHealthUI_Refactor>();

        meleeWeaponScript = GetComponent<MeleeWeapon>();
        meleeWeaponScript.AttackDamage = model.meleeAttackDamage;
        meleeWeaponScript.attackRange = model.meleeAttackRange;

        rangedWeaponScript = GetComponent<RangedWeapon>();
        rangedWeaponScript.damage = model.rangedAttackDamage;
        rangedWeaponScript.speed = model.rangedAttackProjectileSpeed;

        
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverController>();
    }

    void Update()
    {
        if (PauseMenuController.GameIsPaused == false&&view.animator!=null)
        {
            model.newPosition = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            // Check if player should be dead
            if (model.health <= 0.0f)
            {
                FindObjectOfType<AudioManager>().Play("Player Dead");
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

            if (Time.time >= model.nextAttackTime)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    FindObjectOfType<AudioManager>().Play("Melee");
                    view.swordAnimator.SetTrigger("Attack");
                    meleeWeaponScript.Attack();
                    model.nextAttackTime = Time.time + 1f / model.meleeAttackRate;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    FindObjectOfType<AudioManager>().Play("Shoot");
                    rangedWeaponScript.Attack(model.rangedAttackPattern.ToString());
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
        FindObjectOfType<AudioManager>().Play("Health Gained");
    }

    /**
     * This method decreases the health attribute by the damage parameter
     */
    public void DamagePlayer(float damage)
    {
        FindObjectOfType<AudioManager>().Play("Player Hit");
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
}
