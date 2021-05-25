using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //[SerializeField] private string _mapName;

    private string[] campaignPaths;

    private string[] freeplayPaths;

    private Mode _mode;

    private int _campaignIndex;

    private Level _level;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level manager start");
        _mode = Mode.CAMPAIGN;
        _campaignIndex = 0;
        _mapProvider = new MapProvider();
        GetMapPaths();
        _level = new Level(_cam);
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        _level.Update();
        if (_level.Finished)
        {
            switch (_mode)
            {
                case Mode.CAMPAIGN:
                    if (_campaignIndex == campaignPaths.Length - 1)
                        Exit();
                    else
                        NextLevel();
                    break;
                case Mode.FREEPLAY:
                    FreeplayMenu();
                    break;
            }
        }
    }

    private void FreeplayMenu()
    {
        throw new NotImplementedException();
    }

    private void NextLevel()
    {
        _mapProvider.LoadFromDisk(campaignPaths[_campaignIndex]);
        Debug.Log(_mapProvider.MapInfos);
        _level.Start(_mapProvider.MapInfos);
    }

    private void Exit()
    {
        SceneManager.LoadScene(_exitScene);
    }
    private void GetMapPaths()
    {
        campaignPaths = Directory.GetFiles(mapDir + "Campaign/");
        freeplayPaths = Directory.GetFiles(mapDir + "Freeplay/");
    }
}
