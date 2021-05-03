using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public struct GeneratedMapOptions
    {
        public int rows;
        public int columns;
    }
    public struct MapOptions
    {
        public bool generated;
        public GeneratedMapOptions generatedMapOptions;
        public string mapName;
    }

    private MapProvider mapProvider;

    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        mapProvider = ScriptableObject.CreateInstance<MapProvider>();
        mapProvider.LoadFromDisk("map_1");
        Debug.Log("Map loaded");
        board = GameObject.Find("Board").GetComponent<Board>();
        board.SetMap(mapProvider.Map, mapProvider.RowsLength, mapProvider.ColumnsLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createMap(MapOptions options)
    {
        if (options.generated)
        {
            mapProvider.generate(options.generatedMapOptions);
        } else
        {
            mapProvider.LoadFromDisk(options.mapName);
        }

    }
}
