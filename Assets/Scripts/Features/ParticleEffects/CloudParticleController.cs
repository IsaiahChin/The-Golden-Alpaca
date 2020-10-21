using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is used to destroy the Cloud Particle Effect GameObject 
 * after a certain period of time
 */
public class CloudParticleController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }
}
