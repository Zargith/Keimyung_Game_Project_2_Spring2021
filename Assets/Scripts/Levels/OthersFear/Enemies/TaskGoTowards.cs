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
        DOWN,
        NONE
    }

    [SerializeField] private Direction GoTo = Direction.NONE;

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
        GetComponent<SpriteRenderer>().enabled = false;
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
