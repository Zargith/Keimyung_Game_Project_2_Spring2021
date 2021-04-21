using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TaskGoTowards task;
        if (collision.gameObject.TryGetComponent<TaskGoTowards>(out task))
        {
            Vector2 new_v = rb.velocity;
            switch (task.GetDirection())
            {
                case TaskGoTowards.Direction.LEFT:
                    new_v.x = speed;
                    break;
                case TaskGoTowards.Direction.RIGHT:
                    new_v.x = -speed;
                    break;
                case TaskGoTowards.Direction.UP:
                    break;
                case TaskGoTowards.Direction.DOWN:
                    break;
            }
            rb.velocity = new_v;
        }
    }
}
