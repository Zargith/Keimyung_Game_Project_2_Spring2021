using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string gameEntryPointScene;

    public void Play()
    {
        SceneManager.LoadScene(gameEntryPointScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
