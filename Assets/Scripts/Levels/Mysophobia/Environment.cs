using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, PositionableGraphic
{
    public List<GameObject> objs;
    public GameObject InstallerPrefab;

    public GameObject CoronaPrefab;

    public GameObject PlaguePrefab;

    public GameObject FluPrefab;

    private PositionProvider _pp;

    private Transform boardTranform; // useful ?

    List<GameObject> prefabList;

    //Vector3[4] postions; 

    void Start()
    {
        boardTranform = GameObject.Find("Board").GetComponent<Transform>();
        prefabList = new List<GameObject>() { InstallerPrefab, CoronaPrefab, PlaguePrefab, FluPrefab};
    }
    public void SetupPositionProvider(PositionProvider pp)
    {
        _pp = pp;
    }

    public void Draw()
    {
        int rand;

        while (prefabList.Count > 0)
        {
            rand = Random.Range(0, prefabList.Count - 1);

        }
        
    }
}
