using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour, PositionableGraphic
{
    public GameObject pathSquarePrefab;

    public GameObject blockSquarePrefab;

    public GameObject endSquarePrefab;

    public GameObject playerPrefab;

    public GameObject virusPrefab;

    private Transform boardOriginTransform;

    private PositionProvider _pp;

    public byte[,] Map { private get; set; }
    public Vector2 playerStartPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        boardOriginTransform = GameObject.Find("Board").GetComponent<Transform>();
    }

    public void SetupPositionProvider(PositionProvider pp)
    {
        _pp = pp;
    }
    public void Draw()
    {
        int rows = _pp.MapSize.x;
        int cols = _pp.MapSize.y;

        Debug.Log("Start drawing map: rows: " + rows + " cols: " + cols);
        GameObject newObj;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Map[i, j] == 0)
                {
                    newObj = Instantiate(blockSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                    newObj.transform.parent = boardOriginTransform;
                    
                } else if (Map[i, j] == 1 || Map[i, j] == 4)
                {
                   newObj = Instantiate(pathSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                   if (Map[i, j] == 4)
                    {
                        playerStartPos = new Vector2(i, j);
                    }
                   
                } else if (Map[i, j] == 2)
                {
                    newObj = Instantiate(endSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                    //newObj.transform.position = new Vector3(i, 0, j);
                }
                else
                {
                    Debug.Log("Problem in a square value of the map: " + Map[i, j]);
                }
            }
        }
        Debug.Log("Finished");
    }

    public void playerSpawn()
    {
        Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
    }

}
