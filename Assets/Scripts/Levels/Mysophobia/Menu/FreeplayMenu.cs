using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeplayMenu : MonoBehaviour
{
    private LevelManager _manager;

    public void Start()
    {
        _manager = GameObject.Find("Root").GetComponent<LevelManager>();
    }

    public void Campaign1()
    {
        _manager.LoadFreeplayMap("campaign_1");
    }

    public void Campaign2()
    {
        _manager.LoadFreeplayMap("campaign_2");
    }

    public void Campaign3()
    {
        _manager.LoadFreeplayMap("campaign_3");
    }
}
