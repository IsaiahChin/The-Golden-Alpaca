﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Author: MatthewCopeland
public class MouseFollow : MonoBehaviour
{
    public Camera mainCamera;
    public bool showSceneLabels;

    private Vector3 clickPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        clickPosition = -Vector3.one;

        Plane plane = new Plane(Vector3.up, 0f);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        if (plane.Raycast(ray,out distanceToPlane))
        {
            clickPosition=ray.GetPoint(distanceToPlane);
        }
        
        Vector3 direction = clickPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels)
        {
            //Draw sphere from the view point of the size of the view range
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, new Vector3(clickPosition.x, 1, clickPosition.z));
            Handles.Label(new Vector3(clickPosition.x, 1, clickPosition.z), "Player Aim");
        }
    }
}