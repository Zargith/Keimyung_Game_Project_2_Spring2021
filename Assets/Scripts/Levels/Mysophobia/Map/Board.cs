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
    //private byte[,] _initialMap;

    private byte[,] _map;

    public bool PassThroughVirus = false;

    //private bool _firstPassThrough = true;

    private List<GameObject> _squareInstance;

    private Player _player;

    private GameObject _virusPrefab;

    private Dictionary<Vector2Int, GameObject> _virusInstances;

    public override void Init(PositionProvider pp)
    {
        _pp = pp;

        ReceivePrefab("Board");

        _virusPrefab = GetPrefab("Virus");
        _squareInstance = new List<GameObject>();
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
        GameObject obj;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                worldPos = BoardToWorldPos(new Vector2Int(i, j));
                if (_map[i, j] == 0){
                    obj = Instantiate(blockSquarePrefab, worldPos, Quaternion.identity);                    
                } else if (_map[i, j] == 1 || _map[i, j] == 4)
                {
                   obj = Instantiate(pathSquarePrefab, worldPos, Quaternion.identity);
                   if (_map[i, j] == 4)
                    {
                        obj.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                        _player = new Player(GetPrefab("Player"), new Vector2Int(i, j));
                        _player.Instance = Instantiate(_player.Prefab, worldPos, Quaternion.identity);
                    }
                } else if (_map[i, j] == 2)
                {
                    obj = Instantiate(endSquarePrefab, worldPos, Quaternion.identity);
                } else
                {
                    throw new Exception("Problem in a square value of the map: " + _map[i, j]);
                }
                _squareInstance.Add(obj);
            }
        }
    }

    public override void Cleanup()
    {
        foreach (GameObject g in _squareInstance)
            Destroy(g);
        _squareInstance.Clear();
        foreach (KeyValuePair<Vector2Int, GameObject> kvp in _virusInstances)
            Destroy(kvp.Value);
        _virusInstances.Clear();
        Destroy(_player.Instance);
        //_map = _initialMap;
    }

    public void SetMap(byte[,] map)
    {
        //_initialMap = map;
        _map = map;
    }

    public void Reset()
    {
        MoveEntity(_player.Instance, _player.InitialPos);
        _player.ResetPos();
        foreach (KeyValuePair<Vector2Int, GameObject> kvp in _virusInstances)
        {
            _map[kvp.Key.x, kvp.Key.y] = 1;
            Destroy(kvp.Value);
        }
        _virusInstances.Clear();
        //_map = _initialMap;
    }

    public bool IsWin()
    {
        if (_map[_player.BoardPos.x, _player.BoardPos.y] == 2)
            return true;
        return false;
    }

    public bool MovePlayer(Direction direction)
    {
        return _player.Move(direction, MoveEntity);
    }

    public void SpawnVirus(Direction directionFromPlayer)
    {
        Vector2Int pos = GetPlayerSidePos(directionFromPlayer);

        if (IsPositionAvailable(pos))
        {
            Debug.Log("Spawn virus: " + pos);
            _map[pos.x, pos.y] = 3;
            _virusInstances.Add(pos, Instantiate(_virusPrefab, BoardToWorldPos(pos), Quaternion.identity));
        }
    }

    public bool DeleteVirus()
    {
        Debug.Log("Delete virus");
        Vector2Int pos = GetPlayerSidePos(_player.Direction);
        GameObject obj;

        Debug.Log(pos); 
        if (IsPositionAVirus(pos))
        {
            Debug.Log("lol");
            obj = _virusInstances[pos];
            Debug.Log(obj);
            _virusInstances.Remove(pos);
            Destroy(obj);
            _map[pos.x, pos.y] = 1;
            return (true);
        }
        return (false);
    }

    private bool MoveEntity(GameObject entity, Vector2Int boardPos)
    {
        Debug.Log("Entity try move: " + boardPos);
        if (boardPos.x < 0 || boardPos.x >= _pp.MapSize.x || boardPos.y < 0 || boardPos.y >= _pp.MapSize.y)
        {
            Debug.Log("Move failed: Side map");
            return (false);
        }
        if (_map[boardPos.x, boardPos.y] == 0)
        {
            Debug.Log("Move failed: Wall");
            return (false);
        }
        if (_map[boardPos.x, boardPos.y] == 3 && !PassThroughVirus)
        {
            /*if (_firstPassThrough)
            {
                _firstPassThrough = false;
            }*/
            Debug.Log("Move failed: Virus");
            return (false);
        }
        Debug.Log("Move succeed");
        entity.transform.position = BoardToWorldPos(boardPos);
        return (true);
    }

    private Vector2 BoardToWorldPos(Vector2Int pos)
    {
        int tempOffset = _pp.MapSize.y - 1; // TODO regularize

        return (new Vector2(pos.y, -pos.x + tempOffset));
    }

    private Vector2Int WorldToBoardPos(Vector2 pos)
    {
        int tempOffset = _pp.MapSize.y - 1; // TODO regularize

        return (new Vector2Int(-((int)(pos.y - tempOffset)), (int)pos.x));
    }

    private Vector2Int GetPlayerSidePos(Direction direction)
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

    private bool IsPositionAVirus(Vector2Int boardPos)
    {
        return IsPositionValid(boardPos) && _map[boardPos.x, boardPos.y] == 3;
    }

    private bool IsPositionAvailable(Vector2Int boardPos)
    {
        return IsPositionValid(boardPos) && _map[boardPos.x, boardPos.y] == 1;
    }

    private bool IsPositionValid(Vector2Int boardPos)
    {
        return !(boardPos.x < 0 || boardPos.x >= _pp.MapSize.x || boardPos.y < 0 || boardPos.y >= _pp.MapSize.y);
    }

    /*private IEnumerator popPassThroughText()
    {
        GameObject container = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "");

        container.SetActive(true);
        yield return new WaitForSeconds(3);
        container.SetActive(false);
        yield return null;
    }*/
}
