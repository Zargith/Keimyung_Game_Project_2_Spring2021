using System;
using UnityEngine;

public class Player
{
    public GameObject _prefab { get; private set; }

    public GameObject _instance;
    public Board.Direction Direction { get; private set; }

    public Vector2Int InitialPos { get; private set; }
    public Vector2Int BoardPos { get; private set; }
    public Player(GameObject prefab, Vector2Int boardPos)
    {
        _prefab = prefab;
        BoardPos = boardPos;
        InitialPos = boardPos;
        Direction = Board.Direction.UP;
    }

    public bool Move(Board.Direction direction, Func<GameObject, Vector2Int, bool> moveFunction)
    {
        Debug.Log("Player Try move: " + direction);
        Vector2Int newPos;

        Direction = direction;
        newPos = direction switch
        {
            Board.Direction.UP => new Vector2Int(BoardPos.x - 1, BoardPos.y),
            Board.Direction.DOWN => new Vector2Int(BoardPos.x + 1, BoardPos.y),
            Board.Direction.RIGHT => new Vector2Int(BoardPos.x, BoardPos.y + 1),
            Board.Direction.LEFT => new Vector2Int(BoardPos.x, BoardPos.y - 1),
            _ => throw new Exception("Player move: Bad direction: " + direction),
        };
        if (moveFunction(_instance, newPos))
        {
            BoardPos = newPos;
            return (true);
        }
        return (false);
    }
}
