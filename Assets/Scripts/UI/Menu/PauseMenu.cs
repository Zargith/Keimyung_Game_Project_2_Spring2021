using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    [SerializeField] string startMenuScene;

    public void Resume()
    {
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
}
