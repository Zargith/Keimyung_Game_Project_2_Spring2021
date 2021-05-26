using UnityEngine;

public class MysophobiaMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MysophobiaMenu;

    private bool _active;

    // Update is called once per frame
    void Update()
    {
        if (!MysophobiaMenu.activeSelf && _active)
            Unpause();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_active)
                Unpause();
            else
                Pause();
        }

        if (_active)
            return;
    }

    void Pause()
    {
        Time.timeScale = 0;
        FindInactiveChild("GameModeMenu").SetActive(true);
        FindInactiveChild("FreeplayMenu").SetActive(false);
        MysophobiaMenu.SetActive(true);
        _active = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        MysophobiaMenu.SetActive(false);
        _active = false;
    }

    public void DisplayFreeplayMenu()
    {
        FindInactiveChild("GameModeMenu").SetActive(false);
        FindInactiveChild("FreeplayMenu").SetActive(true);
        MysophobiaMenu.SetActive(true);
        _active = true;
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
