using UnityEngine;

public class Level
{
    public enum State
    {
        WIN,
        LOOSE
    }

    private Camera _cam;

    private const int DEFAULT_LIVES = 50;

    private int _lives = DEFAULT_LIVES;

    private int _turn;

    public bool Loaded { get; private set; }
    public bool Finished { get; private set; }

    public State Status { get; private set; }
        
    private Board _board;

    private Environment _environment;

    private ActionQueue _actionQueue;

    private InputAction _inputAction;

    private SpellManager _spellManager;

    private DisplayManager _displayManager;

    private Map _mapInfos;

    public Level(Camera cam)
    {
        _cam = cam;

        _inputAction = new InputAction();
        _board = ScriptableObject.CreateInstance<Board>();
        _environment = ScriptableObject.CreateInstance<Environment>();
        _actionQueue = ScriptableObject.CreateInstance<ActionQueue>();
    } 
    public void Start(Map mapInfos)
    {
        Debug.Log("Start a new level");
        PositionProvider pp;

        Loaded = true;
        _mapInfos = mapInfos;
        _turn = 0;
        Finished = false;
        Debug.Log(mapInfos.Lives);
        _lives = mapInfos.Lives;

        pp = new PositionProvider(new Vector2(0, 0), new Vector2Int(mapInfos.Rows, mapInfos.Cols), 1); // tmp

        _board.Init(pp);
        _environment.Init(pp);
        _actionQueue.Init(pp);

        _actionQueue.SetMaxAction(_lives);

        /*LINK GRAPHIC AND MAP*/

        _board.SetMap(mapInfos.Data);

        /*SPELL*/

        _spellManager = new SpellManager();
        _spellManager.Add(InputAction.Spell.ACCELERATION, new Spell(InputAction.Spell.ACCELERATION, Spell.ReloadType.COOLDOWN, mapInfos.AccelerationCD, false));
        _spellManager.Add(InputAction.Spell.DELETE_VIRUS, new Spell(InputAction.Spell.DELETE_VIRUS, Spell.ReloadType.USAGE, mapInfos.SprayUses, true));
        _spellManager.AttachDisplay(InputAction.Spell.ACCELERATION, "AccelerationSpell");
        _spellManager.AttachDisplay(InputAction.Spell.DELETE_VIRUS, "AntivirusSpraySpell");

        /*DRAW*/

        _board.Draw();
        _environment.Draw();
        _actionQueue.Draw();

        /*CAMERA*/

        PositionCamera(pp);

        /*DISPLAY*/

        _displayManager = new DisplayManager();
        _displayManager.AddText("LivesText", "Lives");
        _displayManager.Texts["Lives"].Update(_lives.ToString());
        }

    public void Update()
    {
        if (_turn == 0)
        {
            PlayerTurn();
        }
        else
        {
            if (_board.IsWin())
            {
                FinishGame(State.WIN);
            }
            VirusTurn();
        }
    }

    public void Clean()
    {
        Debug.Log("Clean level");
        _board.Cleanup();
        _environment.Cleanup();
        _actionQueue.Cleanup();
        Loaded = false;
    }
    private void PlayerTurn()
    {
        InputManager.GetInput(_inputAction);

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

                if (spell.Activated)
                {
                    Debug.Log("Cancel spell: " + spell.type);
                    spell.Cancel();
                    break;
                }
                if (!spell.IsAvailable())
                {
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
                }
                else
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
        _lives--;
        _displayManager.Texts["Lives"].Update(_lives.ToString());
        if (_lives == 0)
            FinishGame(State.LOOSE);
    }

    public void ResetGame()
    {
        Debug.Log("Retry");
        _board.Reset();
        _actionQueue.Reset();
        _spellManager.Reset();
        _lives = _mapInfos.Lives;
        Finished = false;
        _displayManager.Texts["Lives"].Update(_lives.ToString());
    }

    private void FinishGame(State status)
    {
        Debug.Log("Game finished");
        Finished = true;
        Status = status;
    }

    private void PositionCamera(PositionProvider pp)
    {
        _cam.transform.position = new Vector3(pp.Middle.x, pp.Middle.y - 0.5f, _cam.transform.position.z);

        float scaleOrthoSize = (pp.MapSize.x + pp.MapSize.y) / 2 * 0.9f;
        _cam.orthographicSize = 11 < scaleOrthoSize ? scaleOrthoSize : 11;
    }

}
