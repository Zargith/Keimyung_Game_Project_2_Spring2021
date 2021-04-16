using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayIfPlayerIsInZone : MonoBehaviour
{
    public GameObject elemToDisplay;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            elemToDisplay.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
            elemToDisplay.SetActive(false);
    }
}
