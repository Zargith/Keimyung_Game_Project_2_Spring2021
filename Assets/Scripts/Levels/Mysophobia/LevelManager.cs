using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    bool quit;

    MapProvider mapProvider;

    Board board;

    Environment environment;

    // Start is called before the first frame update
    void Start()
    {
        quit = false;

        mapProvider = ScriptableObject.CreateInstance<MapProvider>();

        board = GameObject.Find("Board").GetComponent<Board>();
        environment = GameObject.Find("Environment").GetComponent<Environment>();

        launchGame("map_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchGame(string mapName)
    {
        PositionProvider pp; 
        
        /*LOAD MAP */

        mapProvider.LoadFromDisk(mapName);

        /* SET POS */

        pp = new PositionProvider(new Vector2(0, 0), new Vector2Int(mapProvider.RowsLength, mapProvider.ColumnsLength), 1); // tmp

        board.SetupPositionProvider(pp);
        environment.SetupPositionProvider(pp);

        /*LINK GRAPHIC AND INTERN*/

        board.Map = mapProvider.Map;

        /*DRAW*/

        board.Draw();
        environment.Draw();

        /*ADDITIONALS INIT*/

        board.playerSpawn();
        //Init action queue

        /*GAME LOOP*/

        gameLoop();
    }

    private void gameLoop()
    {
        while (!quit)
        {
            // Input
            // Activated power ?
            // Wait move
            // Virus turn
            // Pop back queue
            // Visual env effect
            // Apply (virus pop or not)
            // Visual maj queue
            // decrease max turn (?)
            // next turn
        }
    }
}
