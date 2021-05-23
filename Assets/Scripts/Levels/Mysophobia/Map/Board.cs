using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : PositionableGraphic
{
    public enum Direction
    {
        UP = 0,
        DOWN = 1,
        RIGHT = 2,
        LEFT = 3
    }
    public byte[,] Map { private get; set; }

    private Player _player;

    private GameObject _virusPrefab;

    private Dictionary<Vector2Int, GameObject> _virusInstances;

    public override void Init(PositionProvider pp)
    {
        _pp = pp;

        ReceivePrefab("Board");

        _virusPrefab = GetPrefab("Virus");
        _virusInstances = new Dictionary<Vector2Int, GameObject>();
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
                if (Map[i, j] == 0){
                    Instantiate(blockSquarePrefab, worldPos, Quaternion.identity);                    
                } else if (Map[i, j] == 1 || Map[i, j] == 4)
                {
                   var obj = Instantiate(pathSquarePrefab, worldPos, Quaternion.identity);
                   if (Map[i, j] == 4)
                    {
                        obj.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                        _player = new Player(GetPrefab("Player"), new Vector2Int(i, j));
                        _player._instance = Instantiate(_player._prefab, worldPos, Quaternion.identity);
                    }
                } else if (Map[i, j] == 2)
                {
                    Instantiate(endSquarePrefab, worldPos, Quaternion.identity);
                } else
                {
                    Debug.Log("Problem in a square value of the map: " + Map[i, j]);
                }
            }
        }
        Debug.Log("Finished");
    }

    public void Reset()
    {
        moveEntity(_player._instance, _player.InitialPos);
        foreach (KeyValuePair<Vector2Int, GameObject> kvp in _virusInstances)
        {
            Map[kvp.Key.x, kvp.Key.y] = 1;
            Destroy(kvp.Value);
        }
        _virusInstances.Clear();
    }

    public bool MovePlayer(Direction direction)
    {
        return _player.Move(direction, moveEntity);
    }

    public void SpawnVirus(Direction directionFromPlayer)
    {
        Vector2Int pos = getPlayerSidePos(directionFromPlayer);

        if (isPositionAvailable(pos))
        {
            Debug.Log("Spawn virus: " + pos);
            Map[pos.x, pos.y] = 3;
            _virusInstances.Add(pos, Instantiate(_virusPrefab, boardToWorldPos(pos), Quaternion.identity));
        }
    }

    public bool DeleteVirus()
    {
        Debug.Log("Delete virus");
        Vector2Int pos = getPlayerSidePos(_player.Direction);
        GameObject obj;

        Debug.Log(pos); 
        if (isPositionAVirus(pos))
        {
            Debug.Log("lol");
            obj = _virusInstances[pos];
            Debug.Log(obj);
            _virusInstances.Remove(pos);
            Destroy(obj);
            Map[pos.x, pos.y] = 1;
            return (true);
        }
        return (false);
    }

    private bool moveEntity(GameObject entity, Vector2Int boardPos)
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

    private Vector2Int getPlayerSidePos(Direction direction)
    {
        Vector2Int playerBoardPos = _player.BoardPos;

        return direction switch
        {
            Direction.UP => new Vector2Int(playerBoardPos.x - 1, playerBoardPos.y),
            Direction.DOWN => new Vector2Int(playerBoardPos.x + 1, playerBoardPos.y),
            Direction.RIGHT => new Vector2Int(playerBoardPos.x, playerBoardPos.y + 1),
            Direction.LEFT => new Vector2Int(playerBoardPos.x, playerBoardPos.y - 1),
            _ => throw new Exception("GetPlayerSidePos: Bad direction: " + direction),
        };
    }

    private bool isPositionAVirus(Vector2Int boardPos)
    {
        return isPositionValid(boardPos) && Map[boardPos.x, boardPos.y] == 3;
    }

    private bool isPositionAvailable(Vector2Int boardPos)
    {
        return isPositionValid(boardPos) && Map[boardPos.x, boardPos.y] == 1;
    }

    private bool isPositionValid(Vector2Int boardPos)
    {
        return !(boardPos.x < 0 || boardPos.x >= _pp.MapSize.x || boardPos.y < 0 || boardPos.y >= _pp.MapSize.y);
    }
}
