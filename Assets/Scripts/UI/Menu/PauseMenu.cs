using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    [SerializeField] string startMenuScene;

    public void Resume()
    {
        Debug.Log("PauseMenu - Resume");
        //TODO
        // Time resume eventually
        // deactive pause panel already did
    }

    public void StartMenu()
    {
        Debug.Log("PauseMenu - Start Menu");
        SceneManager.LoadScene(startMenuScene);
    }
}
