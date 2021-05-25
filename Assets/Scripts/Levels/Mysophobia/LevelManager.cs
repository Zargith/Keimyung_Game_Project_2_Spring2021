using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const int DEFAULT_MAX_TURN = 20;

    [SerializeField] private Camera _cam;

    private MapProvider _mapProvider;

    private Board _board;

    private Environment _environment;

    private ActionQueue _actionQueue;

    [SerializeField] private string _mapName;

    private int _maxTurn = DEFAULT_MAX_TURN;

    private int _turn;

    private InputManager _inputManager;

    private InputAction _inputAction;

    private SpellManager _spellManager;

    private DisplayManager _displayManager;

    // Start is called before the first frame update
    void Start()
    {
        _turn = 0;

        _board = ScriptableObject.CreateInstance<Board>();
        _environment = ScriptableObject.CreateInstance<Environment>();
        _actionQueue = ScriptableObject.CreateInstance<ActionQueue>();

        LaunchGame(_mapName);
    }

    // Update is called once per frame
    void Update()
    {
        if (_turn == 0) 
        {
            PlayerTurn();
        } else
        {
            if (_board.isWin())
            {
                FinishGame();
            }
            VirusTurn();
        }
    }

    private void PlayerTurn()
    {
        _inputManager.GetInput(_inputAction);

        switch (_inputAction.type)
        {
            case InputAction.Type.MOVE:
                if (!_board.MovePlayer((Board.Direction)_inputAction.GetDirection()))
                    break;
                if (!_spellManager.IsActivated(InputAction.Spell.ACCELERATION))
                    _turn = 1;
                else
                    _spellManager.Deactivate(InputAction.Spell.ACCELERATION);
                DecreaseTurn();
                break;
            case InputAction.Type.SPELL:
                var spell = _spellManager.Get(_inputAction.GetSpell());

                if (spell.Activated) {
                    Debug.Log("Cancel spell: " + spell.type);
                    spell.Cancel();
                    break;
                }
                if (!spell.IsAvailable()) {
                    Debug.Log("Spell not available: " + spell.type);
                    break;
                }
                if (spell.Instant)
                {                //TODO a refaire dégueulasse
                    if (spell.type == InputAction.Spell.DELETE_VIRUS)
                    {
                        _board.DeleteVirus();
                    }
                    spell.Use();
                } else
                {
                    if (spell.type == InputAction.Spell.ACCELERATION)
                    {
                        Debug.Log("Activate Accel");
                    }
                    spell.Activate();
                }
                break;
            case InputAction.Type.RETRY:
                ResetGame();
                break;
        }
    }
    private void VirusTurn()
    {
        Debug.Log("Ennemy turn");
        _spellManager.IncreaseTurn();

        EnvironmentVirus.Type type = _actionQueue.Peek();

        _environment.SwapVirusPlace(type);
        _board.SpawnVirus((Board.Direction)_environment.GetInstallerPlace());
        _actionQueue.Next();
        _turn = 0;
    }

    private void DecreaseTurn()
    {
        _maxTurn--;
        _displayManager.Texts["MaxTurn"].Update(_maxTurn.ToString());
        if (_maxTurn == 0)
        {
            Debug.Log("Looser");
            ResetGame();
        }
    }

    private void ResetGame()
    {
        Debug.Log("Retry");
        _board.Reset();
        _actionQueue.Reset();
        _spellManager.Reset();
        _maxTurn = DEFAULT_MAX_TURN;
        _displayManager.Texts["MaxTurn"].Update(_maxTurn.ToString());
    }

    private void FinishGame()
    {
        Debug.Log("Game finished");
        ResetGame();
    }

    public void LaunchGame(string mapName)
    {
        PositionProvider pp;

        /*LOAD MAP */

        _mapProvider = new MapProvider();
        _mapProvider.LoadFromDisk(mapName);

        /* SET POS */

        pp = new PositionProvider(new Vector2(0, 0), new Vector2Int(_mapProvider.RowsLength, _mapProvider.ColumnsLength), 1); // tmp

        _board.Init(pp);
        _environment.Init(pp);
        _actionQueue.Init(pp);

        _actionQueue.SetMaxAction(50);

        /*LINK GRAPHIC AND MAP*/

        _board.Map = _mapProvider.Map;

        /*INPUT*/

        _inputManager = new InputManager();
        _inputAction = new InputAction();

        /*SPELL*/

        _spellManager = new SpellManager();
        

        /*DRAW*/

        _board.Draw();
        _environment.Draw();
        _actionQueue.Draw();

        /*CAMERA*/

        PositionCamera(pp);

        /*DISPLAY*/
        
        _displayManager = new DisplayManager();
        _displayManager.AddText("MaxTurnText", "MaxTurn");
    }

    private void PositionCamera(PositionProvider pp)
    {
        _cam.transform.position = new Vector3(pp.Middle.x, pp.Middle.y - 0.5f, _cam.transform.position.z);

        float scaleOrthoSize = (pp.MapSize.x + pp.MapSize.y) / 2 * 0.9f ;
        _cam.orthographicSize = 11 < scaleOrthoSize ? scaleOrthoSize : 11;
    }
}
