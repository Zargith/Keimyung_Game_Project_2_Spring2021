using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyDecreaseMaxIntensityThroughLevel2D : MonoBehaviour
{
	FireflyDecreaseLight2D scriptDecreaseLight;
	float originalMaxIntensity;
	float percentage = 0f;
	float maxPercentage = 0f;
	public float startPos = 0f;
	public float endPos = 1f;
	public Transform currentPlayerPos;


	void Start()
	{
		scriptDecreaseLight = GetComponent<FireflyDecreaseLight2D>();
		originalMaxIntensity = scriptDecreaseLight.maxIntensity;
	}


	void Update()
	{
		float currentPos = currentPlayerPos.position.x;
		percentage = (currentPos - startPos) / (endPos - startPos);
		if (percentage > maxPercentage && !(percentage > 100)) {
			maxPercentage = percentage;
			float newIntensity = originalMaxIntensity * (1 - maxPercentage);
			if (newIntensity > scriptDecreaseLight.minIntensity)
				scriptDecreaseLight.maxIntensity = newIntensity;
		}
	}
}
