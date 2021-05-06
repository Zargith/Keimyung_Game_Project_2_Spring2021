using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] TaskGoTowards.Direction start_d = TaskGoTowards.Direction.LEFT;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement(start_d);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("colliding");
        TaskGoTowards task;
        if (collision.gameObject.TryGetComponent<TaskGoTowards>(out task))
        {
            movement(task.GetDirection());
        }
    }

    void movement(TaskGoTowards.Direction direction)
    {
        Vector2 n_v = rb.velocity;
        Vector2 n_scale = transform.localScale;
        switch (direction)
        {
            case TaskGoTowards.Direction.LEFT:
                n_v.x = -speed;
                n_scale.x = Mathf.Abs(n_scale.x) * -1;
                break;
            case TaskGoTowards.Direction.RIGHT:
                n_v.x = speed;
                n_scale.x = Mathf.Abs(n_scale.x) * 1;
                break;
            case TaskGoTowards.Direction.UP:
                break;
            case TaskGoTowards.Direction.DOWN:
                break;
        }
        transform.localScale = n_scale;
        rb.velocity = n_v;
    }
}
