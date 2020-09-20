using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFlipping : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    void Update()
    {
        Vector3 clickPosition = -Vector3.one;

        Plane plane = new Plane(Vector3.up, 0f);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        if (plane.Raycast(ray, out distanceToPlane))
        {
            clickPosition = ray.GetPoint(distanceToPlane);
        }

        if (player.transform.position.x<=clickPosition.x)
        {
            Debug.Log("Left");
        }
        else if (player.transform.position.x > clickPosition.x)
        {
            Debug.Log("Right");
        }
    }
}
