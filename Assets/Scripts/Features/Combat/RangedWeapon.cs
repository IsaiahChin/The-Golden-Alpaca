using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    public void Attack()
    {
        FireBullet(attackPoint.forward);
        //Any further expansions/bullet patterns will go here
    }

    /**
    * This method instatiates then fires a bullet
    */
    private void FireBullet(Vector3 direction)
    {
        //Instatiate a bullet object
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<Bullet>().shooterTag = this.tag;
        bullet.GetComponent<Bullet>().damage = this.damage;
        //Get the rigidbody of the bullet object then Apply a force to the bullet object of the bullet speed
        bullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.VelocityChange);
    }
}
