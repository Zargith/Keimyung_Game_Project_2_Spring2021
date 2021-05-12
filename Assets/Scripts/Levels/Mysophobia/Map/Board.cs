using UnityEngine;

public class Board : PositionableGraphic
{
    //private Transform boardOriginTransform;

    public byte[,] Map { private get; set; }
    //public Vector2 playerStartPos { get; private set; }

    public Player _player { get; private set; }

    public Board(PositionProvider pp) : base(pp) {
        ReceivePrefab("Board");
        _player = new Player(GetPrefab("Player"), this);
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
                   Instantiate(pathSquarePrefab, worldPos, Quaternion.identity);
                   if (Map[i, j] == 4)
                    {
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
        Debug.Log("Move succeed");
        entity.transform.position = boardToWorldPos(boardPos);
        return (true);
    }

    public void spawnVirus(Vector2 pos)
    {

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
 }
