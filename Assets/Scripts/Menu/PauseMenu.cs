using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string startMenuScene;

    public string optionsScene;

    public void Resume()
    {
        // TODO
        // Go back (idk how to do right now maybe pause won't be a scene and just a panel with a time stop/active pause panel // resume deactive pause panel time resume
    }

    public void Options()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
}
