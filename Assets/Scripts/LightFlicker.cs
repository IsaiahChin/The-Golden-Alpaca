using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: MatthewCopeland
public class LightFlicker : MonoBehaviour
{
	[Header("Settings")]
	public float baseStart = 0.0f; // start 
	public float amplitude = 1.0f; // amplitude of the wave
	public float phase = 0.0f; // start point inside on wave cycle
	public float frequency = 0.5f; // cycle frequency per second
	
	// Keep a copy of the original color
	private Color originalColor;
	private new Light light;

	// Store the original color
	void Start()
	{
		light = GetComponent<Light>();
		originalColor = light.color;
	}

	void Update()
	{ 
		light.color = originalColor * (Flicker());
	}

	float Flicker()
	{
		float x = (Time.time + phase) * frequency;
		x = x - Mathf.Floor(x); // normalized value (0..1)
		float y = Mathf.Sin(x * 2 * Mathf.PI);
		return (y * amplitude) + baseStart;
	}
}
