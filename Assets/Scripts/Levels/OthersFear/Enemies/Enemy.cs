using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{

    public Vector2 distractedPos = new Vector2(0, 1);
    [SerializeField] float speed = 1.0f;
    [SerializeField] TaskGoTowards.Direction direction = TaskGoTowards.Direction.LEFT;
    Rigidbody2D rb;
    private EnemyState activity = EnemyState.PATROLLING;
    [SerializeReference] UnityEngine.Experimental.Rendering.Universal.Light2D fov;

    public enum EnemyState
    {
        PATROLLING,
        DISTRACTED,
        WATCHING_PALYER,
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement(direction);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindObjectOfType<OthersFearPlayer>().GetComponent<Collider2D>(), true);
        print(fov.pointLightOuterRadius);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.6f * transform.localScale.x, transform.position.y + 1), Vector2.right * transform.localScale.x, fov.pointLightOuterRadius, LayerMask.GetMask("Player"));        // If it hits something...
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            SeePlayer(true);
        }
        else
        {
            SeePlayer(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TaskGoTowards task;
        if (collision.gameObject.TryGetComponent<TaskGoTowards>(out task))
        {
            Movement(task.GetDirection());
        }
    }

    void Movement(TaskGoTowards.Direction n_dir)
    {
        Vector2 n_v = rb.velocity;
        Vector2 n_scale = transform.localScale;
        switch (n_dir)
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
        direction = n_dir;
        transform.localScale = n_scale;
        rb.velocity = n_v;
    }

    public void SeePlayer(bool player_in_fov)
    {
        if (player_in_fov)
        {
            activity = EnemyState.WATCHING_PALYER;
            rb.velocity = Vector2.zero;
        } else
        {
            activity = EnemyState.PATROLLING;
            Movement(direction);
        }
    }

    public void Distracted(bool is_distracted)
    {
        if (is_distracted)
        {
            activity = EnemyState.DISTRACTED;
            fov.color = Color.white;
            rb.velocity = Vector2.zero;
        }
        else
        {
            activity = EnemyState.PATROLLING;
            fov.color = Color.red;
            Movement(direction);
        }
    }
}
