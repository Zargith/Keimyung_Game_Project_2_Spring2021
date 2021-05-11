using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Environment : MonoBehaviour, PositionableGraphic
{
    public GameObject InstallerPrefab;

    public GameObject CoronaPrefab;

    public GameObject PlaguePrefab;

    public GameObject FluPrefab;

    private PositionProvider _pp;

    private Transform boardTranform; // useful ?

    List<GameObject> prefabList;

    Vector2[] positions; 

    void Start()
    {
        boardTranform = GameObject.Find("Board").GetComponent<Transform>();
        
    }
    public void SetupPositionProvider(PositionProvider pp)
    {
        _pp = pp;
        positions = new[] { _pp.getSidePointRelativeToMiddle(PositionProvider.Side.X),
                            _pp.getSidePointRelativeToMiddle(PositionProvider.Side.Y),
                            _pp.getSidePointRelativeToMiddle(PositionProvider.Side.X, true),
                            _pp.getSidePointRelativeToMiddle(PositionProvider.Side.Y, true)};
        positions[0].x += 3;
        positions[1].y += 3;
        positions[2].x -= 3;
        positions[3].y -= 3;
    }

    public void Draw()
    {
        prefabList = new List<GameObject>() { InstallerPrefab, CoronaPrefab, PlaguePrefab, FluPrefab };
        Random.InitState((int)DateTime.Now.Ticks);

        int rand;
        GameObject elem;
        int positionIndex = 0;
        Quaternion demiAngle = Quaternion.identity;

        demiAngle.x = 90;
        demiAngle.y = 90;
        while (prefabList.Count > 0)
        {
            rand = Random.Range(0, prefabList.Count - 1);
            elem = prefabList[rand];
            prefabList.RemoveAt(rand);
            if (positionIndex % 2 == 0)
                Instantiate(elem, positions[positionIndex], Quaternion.identity);
            else
                Instantiate(elem, positions[positionIndex], demiAngle);
            positionIndex++;
        }
        
    }
}
