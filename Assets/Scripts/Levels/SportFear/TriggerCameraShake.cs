using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
    public float duration;
    public float amount;
    CameraShake cam;
    PlayerSportController psc;

    private void Start()
    {
        cam = GameObject.FindObjectOfType<CameraShake>();
        psc = GameObject.FindObjectOfType<PlayerSportController>();
    }

    void Shake()
    {
        StartCoroutine(cam.Shake(duration, amount));
        psc.shake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            psc.dieByRock();
        }
    }

}
