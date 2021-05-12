using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenu;

    private bool pauseActive;

    void Update()
    {
        if (!PauseMenu.activeSelf && pauseActive)
            Unpause();
        if (Input.GetButtonUp("Pause"))
        {
            if (pauseActive)
                Unpause();
            else
                Pause();
        }

        if (pauseActive)
            return;
    }

    void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        pauseActive = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        pauseActive = false;
    }
}
