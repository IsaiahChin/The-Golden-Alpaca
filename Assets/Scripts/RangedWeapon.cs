﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: MatthewCopeland
public class RangedWeapon : MonoBehaviour
{
	[Header("Resources")]
	//Prefab of the bullet object to be fired
    public GameObject bulletPrefab;

	//Attack point from where the bullet is fired from
	public Transform attackPoint;

	[Header("Attack Stats")]
	//Speed that the bullet should travel at
	public float speed = 10f;
	public float damage = 1;

    public void Attack()
	{
		//Instatiate a bullet object
		GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
		bullet.GetComponent<Bullet>().shooterTag = this.tag;
		bullet.GetComponent<Bullet>().damage = this.damage;
		//Get the rigidbody of the bullet object then Apply a force to the bullet object of the bullet speed
		bullet.GetComponent<Rigidbody>().AddForce(attackPoint.forward * speed, ForceMode.VelocityChange);
	}
}