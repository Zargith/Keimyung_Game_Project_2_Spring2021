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

    private int maxTurn = 50;

    private int turn;

    private InputManager _inputManager;

    private InputAction _inputAction;

    private SpellManager _spellManager;

    private DisplayManager _displayManager;

    // Start is called before the first frame update
    void Start()
    {
        quit = false;

        turn = 0;

        mapProvider = ScriptableObject.CreateInstance<MapProvider>();
        board = ScriptableObject.CreateInstance<Board>();
        environment = ScriptableObject.CreateInstance<Environment>();
        actionQueue = ScriptableObject.CreateInstance<ActionQueue>();

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
    }

    private void playerTurn()
    {
        _inputManager.GetInput(_inputAction);

        switch (_inputAction._type)
        {
            case InputAction.Type.MOVE:
                if (!board.MovePlayer((Board.Direction)_inputAction.getDirection()))
                    break;
                if (!_spellManager.IsActivated(InputAction.Spell.ACCELERATION))
                    turn = 1;
                else
                    _spellManager.Deactivate(InputAction.Spell.ACCELERATION);
                decreaseTurn();
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
                        board.DeleteVirus();
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
        board.SpawnVirus((Board.Direction)environment.getInstallerPlace());
        actionQueue.Next();
        turn = 0;
    }

    private void decreaseTurn()
    {
        maxTurn--;
        _displayManager.Texts["MaxTurn"].Update(maxTurn.ToString());
    }



    public void launchGame(string mapName)
    {
        PositionProvider pp; 
        
        /*LOAD MAP */

        mapProvider.LoadFromDisk(mapName);

        /* SET POS */

        pp = new PositionProvider(new Vector2(0, 0), new Vector2Int(mapProvider.RowsLength, mapProvider.ColumnsLength), 1); // tmp

        board.Init(pp);
        environment.Init(pp);
        actionQueue.Init(pp);

        actionQueue.SetMaxAction(50);

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

        /*CAMERA*/

        PositionCamera(pp);

        /*DISPLAY*/
        
        _displayManager = new DisplayManager();
        _displayManager.AddText("MaxTurnText", "MaxTurn");
    }

    private void PositionCamera(PositionProvider pp)
    {
        cam.transform.position = new Vector3(pp.Middle.x, pp.Middle.y - 0.5f, cam.transform.position.z);
        cam.orthographicSize = (pp.MapSize.x + pp.MapSize.y) / 2 * 0.8f;
    }
}
