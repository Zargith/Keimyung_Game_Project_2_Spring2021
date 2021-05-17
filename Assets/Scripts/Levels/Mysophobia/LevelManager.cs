using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private bool quit;

    private MapProvider mapProvider;

    private Board board;

    private Environment environment;

    private ActionQueue actionQueue;

    [SerializeField] private string mapName;

    private int turn;

   // private EnvironmentVirus.Type _nextVirusAction;

    // Start is called before the first frame update
    void Start()
    {
        quit = false;

        turn = 0;

        mapProvider = ScriptableObject.CreateInstance<MapProvider>();

        //board = GameObject.Find("Board").GetComponent<Board>();
        //environment = GameObject.Find("Environment").GetComponent<Environment>();

        launchGame(mapName);
    }

    // Update is called once per frame
    void Update()
    {
        if (turn == 0) 
        {
            playerTurn();
        } else
        {
            virusTurn();
        }

            
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

    private void playerTurn()
    {
        bool ret = false;
        bool acceleration = false;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ret = board._player.Move(Player.Movement.DOWN);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ret = board._player.Move(Player.Movement.UP);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ret = board._player.Move(Player.Movement.LEFT);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ret = board._player.Move(Player.Movement.RIGHT);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ret = true;
            acceleration = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ret = board._player.Move(Player.Movement.RIGHT);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            ret = board._player.Move(Player.Movement.RIGHT);
        if (ret)
        {
            if (!acceleration)
                turn = 1;
        }
    }
    private void virusTurn()
    {
        Debug.Log("Ennemy turn");
        EnvironmentVirus.Type type = actionQueue.Peek();

        environment.SwapVirusPlace(type);
        board.spawnVirus(environment.getInstallerPlace());
        actionQueue.Next();
        turn = 0;
    }



    public void launchGame(string mapName)
    {
        PositionProvider pp; 
        
        /*LOAD MAP */

        mapProvider.LoadFromDisk(mapName);

        /* SET POS */

        pp = new PositionProvider(new Vector2(0, 0), new Vector2Int(mapProvider.RowsLength, mapProvider.ColumnsLength), 1); // tmp

        board = new Board(pp);
        environment = new Environment(pp);
        actionQueue = new ActionQueue(pp, 50);

        /*LINK GRAPHIC AND MAP*/

        board.Map = mapProvider.Map;

        /*DRAW*/

        board.Draw();
        environment.Draw();
        actionQueue.Draw();

        PositionCamera(pp);
    }

    private void PositionCamera(PositionProvider pp)
    {
        cam.transform.position = new Vector3(pp.Middle.x, pp.Middle.y - 0.5f, cam.transform.position.z);
        cam.orthographicSize = (pp.MapSize.x + pp.MapSize.y) / 2 * 0.8f;
    }
}
