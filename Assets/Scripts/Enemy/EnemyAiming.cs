using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Author: MatthewCopeland
public class EnemyAiming : MonoBehaviour
{
    //Aim target
    private GameObject target;

    //Scene view info enable/disable
    public bool showSceneLabels;

    /**
     * This method creates a reference to the alpaca 
     */
    private void Start()
    {
        target = GameObject.Find("Alpaca");
    }

    /**
     * This method displays the line of sight from the enemy to the alpaca
     */
    void FixedUpdate()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    /**
     * This method displays the line of sight from the enemy to the alpaca
     */
    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels&& target != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, target.transform.position);
            Handles.Label(target.transform.position, "Aim Target: " + target.name);
        }
    }
}
