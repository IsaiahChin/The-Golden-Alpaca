using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera cam;
    // Update is called once per frame
    void Update()
    {
        Vector3 clickPosition = -Vector3.one;

        Plane plane = new Plane(Vector3.up, 0f);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        if (plane.Raycast(ray,out distanceToPlane))
        {
            clickPosition=ray.GetPoint(distanceToPlane);
        }
        
        Vector3 direction = clickPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
