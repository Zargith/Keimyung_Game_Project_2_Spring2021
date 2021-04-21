using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TaskGoTowards : MonoBehaviour
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    [SerializeField] private Direction GoTo;

    // Start is called before the first frame update
    void Start()
    {
        switch (GoTo)
        {
            case Direction.LEFT:
                transform.localScale = new Vector2(1, 1);
                break;
            case Direction.RIGHT:
                transform.localScale = new Vector2(-1, 1);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Direction GetDirection()
    {
        return GoTo;
    }

}
