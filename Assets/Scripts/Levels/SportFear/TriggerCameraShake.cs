using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
    public float duration;
    public float amount;
    CameraShake cam;

    private void Start()
    {
        cam = GameObject.FindObjectOfType<CameraShake>();
    }

    void Shake()
    {
        StartCoroutine(cam.Shake(duration, amount));
    }


}
