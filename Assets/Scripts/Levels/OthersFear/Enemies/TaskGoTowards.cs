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

    [SerializeField] private Direction GoTo = Direction.RIGHT;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 tmp = transform.localScale;
        switch (GoTo)
        {
            case Direction.LEFT:
                tmp.x = Mathf.Abs(tmp.x) * -1;
                break;
            case Direction.RIGHT:
                tmp.x = Mathf.Abs(tmp.x) * 1;
                break;
            default:
                break;
        }
        transform.localScale = tmp;
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
