
using UnityEngine;

public class GameModeMenu : MonoBehaviour
{
    private LevelManager _manager;

    public void Start()
    {
        _manager = GameObject.Find("Root").GetComponent<LevelManager>();
    }
    public void CampaignMode()
    {
        _manager.StartCampaign();
    }

    public void FreeplayMode()
    {
    }
}
