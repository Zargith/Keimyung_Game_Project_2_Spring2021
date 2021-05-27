using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public enum Mode
    {
        CAMPAIGN,
        FREEPLAY
    }

    private const string mapDir = "Assets/Resources/Mysophobia/Maps/";

    [SerializeField] private Camera _cam;

    [SerializeField] private string _exitScene;

    private MapProvider _mapProvider;

    private string[] campaignPaths;

    private string[] freeplayPaths;

    private Mode _mode;

    private int _campaignIndex;

    private Level _level;

    private GameObject _mysophobiaMenu;

    private GameObject _loseMenu;

    private bool _loseMenuOpen;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level manager start");
        _mapProvider = new MapProvider();
        _mysophobiaMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "MysophobiaMenu");
        _loseMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "LoseMenu");
        _loseMenuOpen = false;
        GetMapPaths();
        _mysophobiaMenu.GetComponentInChildren<ListCreator>().Draw(GetShortFileNames(freeplayPaths), freeplayPaths);
        _level = new Level(_cam);
        StartCampaign(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_level.Finished)
        {
            if (_level.Status == Level.State.WIN)
            {
                switch (_mode)
                {
                    case Mode.CAMPAIGN:
                        if (_campaignIndex == campaignPaths.Length - 1)
                            Exit();
                        else
                            NextLevel(true);
                        break;
                    case Mode.FREEPLAY:
                        _mysophobiaMenu.GetComponentInChildren<MysophobiaMenuManager>().DisplayFreeplayMenu();
                        break;
                }
            } else
            {
                if (!_loseMenuOpen)
                    LoseMenu();
            }
        } else
        {
            _level.Update();
        }
    }

    public void StartCampaign(bool cleanup = true)
    {
        if (_mode == Mode.CAMPAIGN && _level.Loaded)
            return;
        _mode = Mode.CAMPAIGN;
        _campaignIndex = 0;
        NextLevel(cleanup);
    }

    public void LoadFreeplayMap(string path)
    {
        _mode = Mode.FREEPLAY;
        _level.Clean();
        _mapProvider.LoadFromDisk(path);
        _level.Start(_mapProvider.MapInfos);
    }

    public void Retry()
    {
        Debug.Log("Retry");
        _loseMenuOpen = false;
        _level.ResetGame();
        Time.timeScale = 1;
    }
    private void NextLevel(bool cleanup)
    {
        if (cleanup)
            _level.Clean();
        _mapProvider.LoadFromDisk(campaignPaths[_campaignIndex]);
        _level.Start(_mapProvider.MapInfos);
    }

    private void LoseMenu()
    {
        _loseMenuOpen = true;
        Time.timeScale = 0;
        _loseMenu.SetActive(true);
    }

    private void Exit()
    {
        SceneManager.LoadScene(_exitScene);
    }
    private void GetMapPaths()
    {
        campaignPaths = Directory.GetFiles(mapDir + "Campaign/", "*.txt");
        freeplayPaths = Directory.GetFiles(mapDir + "Freeplay/", "*.txt");
    }

    private string[] GetShortFileNames(string[] paths)
    {
        string[] shortNames = new string[paths.Length];
        int slashIndex;
        int dotIndex;

        for (int i = 0; i < paths.Length; i++)
        {
            slashIndex = paths[i].LastIndexOf('/');
            dotIndex = paths[i].LastIndexOf('.');
            shortNames[i] = paths[i].Substring(slashIndex + 1, dotIndex - slashIndex - 1);
        }
        return (shortNames);
    }
}
