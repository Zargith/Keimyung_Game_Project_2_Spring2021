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

    private InputManager _inputManager;

    private InputAction _inputAction;

    private SpellManager _spellManager;

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
        _inputManager.GetInput(_inputAction);

        switch (_inputAction._type)
        {
            case InputAction.Type.MOVE:
                board._player.Move(_inputAction.getDirection());
                if (!_spellManager.IsActivated(InputAction.Spell.ACCELERATION))
                    turn = 1;
                else
                    _spellManager.Deactivate(InputAction.Spell.ACCELERATION);
                break;
            case InputAction.Type.SPELL:
                var spell = _spellManager.Get(_inputAction.getSpell());

                if (spell.activated) {
                    spell.Deactivate();
                    break;
                }
                if (!spell.isAvailable()) {
                    break;
                }
                if (spell._instant)
                {                //TODO a refaire dégueulasse
                    if (spell._type == InputAction.Spell.DELETE_VIRUS)
                    {
                        board.deleteVirus();
                    }
                    spell.Use();
                } else
                {
                    Debug.Log("Accel");
                    spell.Activate();
                }
                break;
            case InputAction.Type.RETRY:
                break;
        }
    }
    private void virusTurn()
    {
        Debug.Log("Ennemy turn");
        _spellManager.IncreaseTurn();

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

        /*INPUT*/

        _inputManager = new InputManager();
        _inputAction = new InputAction();

        /*SPELL*/

        _spellManager = new SpellManager();
        

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
