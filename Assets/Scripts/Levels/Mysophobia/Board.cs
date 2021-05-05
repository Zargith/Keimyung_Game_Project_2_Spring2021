using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject pathSquarePrefab;

    public GameObject blockSquarePrefab;

    public GameObject endSquarePrefab;

    private Transform boardOriginTransform;

    private byte[,] map;

    private int rows;

    private int cols;

    public Vector2 playerStartPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        boardOriginTransform = GameObject.Find("Board").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMap(byte[,] mapp, int row, int col)
    {
        map = mapp;
        rows = row;
        cols = col;
        Draw();
    }

    private void Draw()
    {
        Debug.Log("Start drawing map: i: " + rows + " j: " + cols);
        GameObject newObj;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 0)
                {
                    newObj = Instantiate(blockSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                    newObj.transform.parent = boardOriginTransform;
                    
                } else if (map[i, j] == 1 || map[i, j] == 4)
                {
                   newObj = Instantiate(pathSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                   if (map[i, j] == 4)
                    {
                        playerStartPos = new Vector2(i, j);
                    }
                   
                } else if (map[i, j] == 2)
                {
                    newObj = Instantiate(endSquarePrefab, new Vector2(i, j), Quaternion.identity, boardOriginTransform);
                    //newObj.transform.position = new Vector3(i, 0, j);
                }
                else
                {
                    Debug.Log("Problem in a square value of the map: " + map[i, j]);
                }
            }
        }
        Debug.Log("Finished");
    }
}
