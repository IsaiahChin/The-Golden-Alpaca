using System.Collections;
using UnityEngine;

//Author: MatthewCopeland
public class RangedWeapon : MonoBehaviour
{
	//Prefab of the bullet object to be fired
    public GameObject bulletPrefab;

    //Attack point from where the bullet is fired from
    public Transform attackPoint { get; set; }
    [HideInInspector]
    public float speed = 10f;
    [HideInInspector]
    public float damage = 0.5f;

    private void Awake()
    {
        attackPoint = this.gameObject.transform.GetChild(1).transform;
    }

    /**
    * This method fires a bullet
    */
    public void Attack(string attackPattern)
    {
        switch (attackPattern)
        {
            case "Default":
                FireBullet(0);
                break;
            case "Shotgun":
                FireBullet(0);
                FireBullet(5);
                FireBullet(355);
                break;
            case "CardinalPoints":
                FireBullet(0);
                FireBullet(45);
                FireBullet(90);
                FireBullet(135);
                FireBullet(180);
                FireBullet(225);
                FireBullet(270);
                FireBullet(315);
                break;
            default:
                break;
        }
    }

    /**
    * This method instatiates then fires a bullet
    */
    private void FireBullet(float rotation)
    {
        //Instatiate a bullet object
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.transform.Rotate(0,rotation,0);
        bullet.GetComponent<Bullet>().shooterTag = this.tag;
        bullet.GetComponent<Bullet>().damage = this.damage;
        //Get the rigidbody of the bullet object then Apply a force to the bullet object of the bullet speed
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * speed, ForceMode.VelocityChange);
    }
}
