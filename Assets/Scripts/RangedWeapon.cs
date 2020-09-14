using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
	//Prefab of the bullet object
    public GameObject bulletPrefab;

	//Fire point
    public Transform firePoint;

	//Attack stats
    public float speed = 50f;

	public Camera cam=Camera.main;


    public void Update()
    {
        cam.sc
    }
    public void Attack()
	{
		cam.scr
		//Instatiate a bullet object
		GameObject ball = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		//Get the rigidbody of the bullet object
		Rigidbody ballRB = ball.GetComponent<Rigidbody>();
		//Apply a force to the bullet object of the bullet speed
		ballRB.AddForce(firePoint.forward * speed, ForceMode.VelocityChange);
	}
}
