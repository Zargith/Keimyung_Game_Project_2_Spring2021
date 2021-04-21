using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoInteractionWithBedEnd : MonoBehaviour
{
    public GameObject elemDisplayed;

    void Update() {
        if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact"))
            SceneManager.LoadScene("Room");
    }
}
