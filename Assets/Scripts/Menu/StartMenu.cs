using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string gameEntryPointScene;

    public string optionsScene;

    public void Play()
    {
        SceneManager.LoadScene(gameEntryPointScene);
    }

    public void Options()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
