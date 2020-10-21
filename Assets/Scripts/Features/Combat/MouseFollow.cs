using UnityEngine;

//Author: MatthewCopeland
public class MouseFollow : MonoBehaviour
{
    private Camera mainCamera;
    public bool showSceneLabels;

    private Vector3 clickPosition;

    private void Start()
    {
        mainCamera = Camera.main;
        showSceneLabels = true;
    }

    void FixedUpdate()
    {
        CalculateClickPosition();
        RotateTowardsClickPosition();
    }

    /**
    * This method rotates the gameobject towards the click position
    */
    private void RotateTowardsClickPosition()
    {
        Vector3 direction = clickPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    /**
    * This method finds the click position in game based on the mouse position on the screen
    */
    private void CalculateClickPosition()
    {
        //sets the click position to an impossible default value
        clickPosition = -Vector3.one;

        //creates a plane at ground level
        Plane plane = new Plane(Vector3.up, 0f);
        //Finds the point on the plane where a raycast would intercept the plane from the mouse
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        //sets the click position
        if (plane.Raycast(ray, out distanceToPlane))
        {
            clickPosition = ray.GetPoint(distanceToPlane);
        }
    }
}
