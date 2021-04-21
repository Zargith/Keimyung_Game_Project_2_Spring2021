using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithBed : MonoBehaviour
{
    [SerializeField] GameObject elemDisplayed;

    void Update() {
        if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
            SceneManager.LoadScene("Tuto");
        }
    }
}