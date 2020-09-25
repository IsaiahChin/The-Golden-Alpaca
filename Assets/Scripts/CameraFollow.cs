using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Author: MatthewCopeland
public class CameraFollow : MonoBehaviour
{
    [Header("Settings")]
    private Transform target;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float smoothingSpeed = 0.125f;
    public bool showSceneLabels;

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        target = GameObject.Find("Alpaca").transform;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothingSpeed);
        transform.position = smoothedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (showSceneLabels&&target!=null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, target.position);
            Handles.Label(target.position,"Follow Target: "+target.name);
        }
    }
}
