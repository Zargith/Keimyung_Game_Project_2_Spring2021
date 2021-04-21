using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnInteractLoadScene : MonoBehaviour
{
    [SerializeField] GameObject elemDisplayed;
    [SerializeField] string sceneName;

    void Update() {
        if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
