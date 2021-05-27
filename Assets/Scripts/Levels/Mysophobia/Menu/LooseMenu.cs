using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _loseMenu;

    [SerializeField] 
    private string startMenuScene;

    private LevelManager _levelManager;

    public void Start()
    {
        _levelManager = GameObject.Find("Root").GetComponent<LevelManager>();
    }

    public void Retry()
    {
        _levelManager.Retry();
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
}
