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

    private const string campaignDir = "Maps/Campaign";

    private const string freeplayDir = "Maps/Freeplay";

    [SerializeField] private Camera _cam;

    [SerializeField] private string _exitScene;

    private MapProvider _mapProvider;

    private readonly string[] campaignPaths = { "campaign_1", "campaign_2", "campaign_3" };

    private string[] freeplayPaths = { "campaign_1", "campaign_2", "campaign_3" };

    private Mode _mode;

    private int _campaignIndex;

    private Level _level;

    [SerializeField] private GameObject _mysophobiaMenu;

    private GameObject _root;

    [SerializeField] private GameObject _loseMenu;

    private bool _loseMenuOpen;


    // Start is called before the first frame update
    void Start()
    {
        _mapProvider = new MapProvider();
        _root = GameObject.Find("Root");
        //_mysophobiaMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "MysophobiaMenu");
        //_loseMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "LoseMenu");
        _loseMenuOpen = false;
        GetMapPaths();
        _mysophobiaMenu.GetComponentInChildren<ListCreator>(true).Draw(freeplayPaths);
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
                        {
                            PlayerPrefs.SetInt("HypochondriacFear", 1);
                            Exit();
                        }
                        else
                        {
                            _campaignIndex++;
                            NextLevel(true);
                        }
                        break;
                    case Mode.FREEPLAY:
                        _root.GetComponent<MysophobiaMenuManager>().DisplayFreeplayMenu();
                        break;
                }
            } else
            {
                if (!_loseMenuOpen)
                {
                    Debug.Log("loose");
                    LoseMenu();
                }
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
        _mapProvider.LoadFromDisk(freeplayDir + '/' + path);
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
        _mapProvider.LoadFromDisk(campaignDir + '/' + campaignPaths[_campaignIndex]);
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
        //campaignPaths = Directory.GetFiles("Assets/Resources/" + mapDir + "Campaign/", "*.txt");
        //freeplayPaths = Directory.GetFiles("Assets/Resources/" + mapDir + "Freeplay/", "*.txt");
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
