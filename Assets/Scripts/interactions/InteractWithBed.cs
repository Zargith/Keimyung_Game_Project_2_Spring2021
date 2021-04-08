using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithBed : MonoBehaviour
{
    public player player;
    public GameObject elemDisplayed;

    void Update() {
        if (elemDisplayed.activeSelf && Input.GetButton("Interact")) {
            if (player.didTuto)
                SceneManager.LoadScene("LevelsHub");
            else
                SceneManager.LoadScene("Tuto");
        }
    }
}
