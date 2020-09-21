using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Author: MatthewCopeland
public class EnemyAiming : MonoBehaviour
{
    public GameObject target;
    public bool showSceneLabels;

    private void Start()
    {
        target = GameObject.Find("Alpaca");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels)
        {
            //Draw sphere from the view point of the size of the view range
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, target.transform.position);
            Handles.Label(target.transform.position, "Aim Target: "+target.name);
        }
    }
}
