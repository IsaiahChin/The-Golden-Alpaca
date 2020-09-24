using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRotateCube : MonoBehaviour
{
    public Vector3 RotateAmount;

    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}
