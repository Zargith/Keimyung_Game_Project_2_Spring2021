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

    public void launchMap(string path)
    {

    }
}
