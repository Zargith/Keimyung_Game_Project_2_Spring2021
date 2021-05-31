using UnityEngine;

public class MysophobiaMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MysophobiaMenu;

    [SerializeField] private GameObject GameCanvas;

    [SerializeField] private GameObject TutoCanvas;

    private bool _menuActive;

    private bool _tutoActive;

    // Update is called once per frame
    void Update()
    {
        if (!MysophobiaMenu.activeSelf && _menuActive)
            HideMenu();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_menuActive)
                HideMenu();
            else
                LaunchMenu();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (_tutoActive)
                HideTuto();
            else
                LaunchTuto();
        }

        if (_menuActive)
            return;
    }

    void LaunchTuto()
    {
        Time.timeScale = 0;
        TutoCanvas.SetActive(true);
        _tutoActive = true;
    }

    void HideTuto()
    {
        Time.timeScale = 1;
        TutoCanvas.SetActive(false);
        _tutoActive = false;
    }
    void LaunchMenu()
    {
        Time.timeScale = 0;
        FindInactiveChild("GameModeMenu").SetActive(true);
        FindInactiveChild("FreeplayMenu").SetActive(false);
        MysophobiaMenu.SetActive(true);
        _menuActive = true;
    }

    void HideMenu()
    {
        Time.timeScale = 1;
        MysophobiaMenu.SetActive(false);
        _menuActive = false;
    }

    public void DisplayFreeplayMenu()
    {
        FindInactiveChild("GameModeMenu").SetActive(false);
        FindInactiveChild("FreeplayMenu").SetActive(true);
        MysophobiaMenu.SetActive(true);
        _menuActive = true;
    }

    private GameObject FindInactiveChild(string name)
    {
        Transform[] transforms = MysophobiaMenu.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in transforms)
        {
            if (t.gameObject.name == name)
                return t.gameObject;
        }
        return null;
    }
}
