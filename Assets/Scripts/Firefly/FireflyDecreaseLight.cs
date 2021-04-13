using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyDecreaseLight : MonoBehaviour
{
	private Light _light;
	public float minIntensity = 0.0f;
	public float maxIntensity = 1.5f;
	public float frequency = 0.01f;
	private bool increasing = true;
	private float intensity = 0f;

	void Start () 
	{
		_light = GetComponent<Light>();
		_light.intensity = intensity;
	}
	
	void Update () 
	{
		if (increasing) {
			intensity += frequency * Time.deltaTime;
			if (intensity >= maxIntensity)
				increasing = false;
		} else {
			intensity -= frequency * Time.deltaTime;
			if (intensity <= minIntensity)
				increasing = true;
		}

		_light.intensity = intensity;
	}
}
