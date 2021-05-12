using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject
{
    public enum Movement
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public enum PowerUp
    {
        ONE,
        TWO
    }

    private GameObject _prefab;

    private GameObject _instance;

    private Board _boardRef;

    public Vector2Int _boardPos { get; set; }
    public Player(GameObject prefab, Board boardRef)
    {
        _prefab = prefab;
        _boardRef = boardRef;
    }

    public void Instanciat(Vector2 pos)
    {
        _instance = Instantiate(_prefab, pos, Quaternion.identity);
    }
    public bool Move(Movement move)
    {
        Debug.Log("Player Try move: " + move);
        Vector2Int newPos = new Vector2Int();
        switch (move)
        {
            case Movement.DOWN:
                newPos = new Vector2Int(_boardPos.x + 1, _boardPos.y);
                break;
            case Movement.UP:
                newPos = new Vector2Int(_boardPos.x - 1, _boardPos.y);
                break;
            case Movement.RIGHT:
                newPos = new Vector2Int(_boardPos.x, _boardPos.y + 1);
                break;
            case Movement.LEFT:
                newPos = new Vector2Int(_boardPos.x, _boardPos.y - 1);
                break;
        }
        if (_boardRef.moveEntity(_instance, newPos))
        {
            _boardPos = newPos;
            return (true);
        }
        return (false);
    }
}
