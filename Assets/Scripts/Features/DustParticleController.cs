using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Alpaca");
    }

    void Update()
    {
        foreach (Transform particleEffect in transform)
        {
            ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emitter = particleSystem.emission;
            emitter.enabled = (player.GetComponent<Rigidbody>().velocity != Vector3.zero);
        }
    }
}
