using UnityEngine;

public class Board : PositionableGraphic
{
    //private Transform boardOriginTransform;

    public byte[,] Map { private get; set; }
    //public Vector2 playerStartPos { get; private set; }

    public Player _player { get; private set; }

    private GameObject _virusPrefab;

    public Board(PositionProvider pp) : base(pp) {
        ReceivePrefab("Board");
        _player = new Player(GetPrefab("Player"), this);
        _virusPrefab = GetPrefab("Virus");
    }

    public override void Draw()
    {
        GameObject blockSquarePrefab = GetPrefab("BlockSquare");
        GameObject pathSquarePrefab = GetPrefab("PathSquare");
        GameObject endSquarePrefab = GetPrefab("EndSquare");

        int rows = _pp.MapSize.x;
        int cols = _pp.MapSize.y;

        Vector2 worldPos;

        Debug.Log("Start drawing map: rows: " + rows + " cols: " + cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                worldPos = boardToWorldPos(new Vector2Int(i, j));
                if (Map[i, j] == 0)
                {
                    Instantiate(blockSquarePrefab, worldPos, Quaternion.identity);
                    //newObj.transform.parent = boardOriginTransform;
                    
                } else if (Map[i, j] == 1 || Map[i, j] == 4)
                {
                   var obj = Instantiate(pathSquarePrefab, worldPos, Quaternion.identity);
                   if (Map[i, j] == 4)
                    {
                        obj.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                        _player._boardPos = new Vector2Int(i, j);
                        Debug.Log("BoardPos: " + new Vector2Int(i, j));
                        Debug.Log("ConvertedWorldPos: " + worldPos);
                        Debug.Log("Reconverted boardPos: " + worldToBoardPos(worldPos));
                        Debug.Log("Start pos: " + _player._boardPos);
                        _player.Instanciat(worldPos);
                    }
                   
                } else if (Map[i, j] == 2)
                {
                    Instantiate(endSquarePrefab, worldPos, Quaternion.identity);
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

    public bool moveEntity(GameObject entity, Vector2Int boardPos)
    {
        Debug.Log("Entity try move: " + boardPos);
        if (boardPos.x < 0 || boardPos.x >= _pp.MapSize.x || boardPos.y < 0 || boardPos.y >= _pp.MapSize.y)
        {
            Debug.Log("Move failed: Side map");
            return (false);
        }
        if (Map[boardPos.x, boardPos.y] == 0)
        {
            Debug.Log("Move failed: Wall");
            return (false);
        }
        if (Map[boardPos.x, boardPos.y] == 3)
        {
            Debug.Log("Move failed: Virus");
            return (false);
        }
        Debug.Log("Move succeed");
        entity.transform.position = boardToWorldPos(boardPos);
        return (true);
    }

    public void spawnVirus(EnvironmentPosition.Placeholder placeholder)
    {
        Vector2Int pos = getPlayerSidePos(placeholder);
        if (isAvailablePosition(pos))
        {
            Map[pos.x, pos.y] = 3;
            Instantiate(_virusPrefab, boardToWorldPos(pos), Quaternion.identity);
        }
    }

    public bool deleteVirus()
    {
        Debug.Log("Delete virus");
        Vector2Int pos = getPlayerSidePos(_player._direction);

        if (Map[pos.x, pos.y] == 3)
        {
            return (true);
        }
        return (false);
    }

    private Vector2 boardToWorldPos(Vector2Int pos)
    {
        int tempOffset = _pp.MapSize.y - 1; // TODO regularize

        return (new Vector2(pos.y, -pos.x + tempOffset));
    }

    private Vector2Int worldToBoardPos(Vector2 pos)
    {
        int tempOffset = _pp.MapSize.y - 1; // TODO regularize

        return (new Vector2Int(-((int)(pos.y - tempOffset)), (int)pos.x));
    }

    private bool isAvailablePosition(Vector2Int boardPos)
    {
        return !(boardPos.x < 0 || boardPos.x >= _pp.MapSize.x || boardPos.y < 0 || boardPos.y >= _pp.MapSize.y) && Map[boardPos.x, boardPos.y] == 1;
    }

    private Vector2Int getPlayerSidePos(EnvironmentPosition.Placeholder placeholder)
    {
        Vector2Int playerBoardPos = _player._boardPos;
        Vector2Int pos = new Vector2Int();

        switch (placeholder)
        {
            case EnvironmentPosition.Placeholder.NORTH:
                pos = new Vector2Int(playerBoardPos.x + 1, playerBoardPos.y);
                break;
            case EnvironmentPosition.Placeholder.SOUTH:
                pos = new Vector2Int(playerBoardPos.x - 1, playerBoardPos.y);
                break;
            case EnvironmentPosition.Placeholder.EAST:
                pos = new Vector2Int(playerBoardPos.x, playerBoardPos.y - 1);
                break;
            case EnvironmentPosition.Placeholder.WEST:
                pos = new Vector2Int(playerBoardPos.x, playerBoardPos.y + 1);
                break;
        }
        return pos;
    }
 }
