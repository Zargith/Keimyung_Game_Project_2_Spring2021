using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoDeadBoundary : MonoBehaviour
{
    public Vector2 respawnPoint;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.transform.position = respawnPoint;
    }
}
