using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyDecreaseMaxIntensityThroughLevel : MonoBehaviour
{
    private FireflyDecreaseLight scriptDecreaseLight;
    private float originalMaxIntensity;
    private float percentage = 0f;
    private float maxPercentage = 0f;
    public float startPos;
    public float endPos;
    public Transform currentPlayerPos;


    void Start()
    {
        scriptDecreaseLight = GetComponent<FireflyDecreaseLight>();
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