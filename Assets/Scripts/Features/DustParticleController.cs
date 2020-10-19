using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class enables all particle effects in a given object when the player is moving
 */

public class DustParticleController : MonoBehaviour
{   
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Alpaca");
    }

    void Update()
    {
        //Loop through each particle system
        foreach (Transform particleEffect in transform)
        {
            //Get reference to the particle system and emission module 
            ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emitter = particleSystem.emission;
            
            //Enable emission module if the player is moving
            emitter.enabled = (player.GetComponent<Rigidbody>().velocity != Vector3.zero);
        }
    }
}
