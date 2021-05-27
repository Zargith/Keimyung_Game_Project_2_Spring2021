using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class OthersFearEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        PATROLLING,
        DISTRACTED,
        WATCHING_PALYER,
    }

    [SerializeField] Vector2 distractedPos = new Vector2(0, 1);
    [SerializeField] float speed = 1.0f;
    [SerializeField] TaskGoTowards.Direction direction = TaskGoTowards.Direction.NONE;
    [SerializeField] string quote;
    [SerializeReference] UnityEngine.Experimental.Rendering.Universal.Light2D fov;
    TextMesh bullyText;

    Rigidbody2D rb;
    private EnemyState activity = EnemyState.PATROLLING;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement(direction);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindObjectOfType<OthersFearPlayer>().GetComponent<Collider2D>(), true);
        bullyText = GetComponentInChildren<TextMesh>();
        if (bullyText)
        {
            bullyText.text = quote;
            bullyText.gameObject.SetActive(false);
            textScale = bullyText.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activity == EnemyState.DISTRACTED)
        {
            if (rb.bodyType == RigidbodyType2D.Kinematic)
                rb.velocity = Vector2.zero;
        }
        else
        {
            SeePlayer();
            Movement(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<TaskGoTowards>(out TaskGoTowards task))
        {
            Movement(task.GetDirection());
        }
    }

    Vector2 textScale;
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
            case TaskGoTowards.Direction.NONE:
                n_v = Vector2.zero;
                break;
        }
        direction = n_dir;
        transform.localScale = n_scale;
        if (bullyText)
            bullyText.transform.localScale = textScale * n_scale;
        if (rb.bodyType == RigidbodyType2D.Kinematic)
            rb.velocity = n_v;
    }

    OthersFearPlayer lastPhit;
    readonly float fovRotOffset = 90;
    float SeePlayer()
    {
        if (fov != null)
        {
            var lookDir = new Vector2(
                Mathf.Cos(Mathf.Deg2Rad * (fov.transform.rotation.eulerAngles.z + fovRotOffset)),
                Mathf.Sin(Mathf.Deg2Rad * (fov.transform.rotation.eulerAngles.z + fovRotOffset)));
            Debug.DrawRay(fov.transform.position, lookDir.normalized * fov.pointLightOuterRadius * 0.85f);
            RaycastHit2D hit = Physics2D.Raycast(fov.transform.position, lookDir.normalized, fov.pointLightOuterRadius * 0.85f, LayerMask.GetMask("Player", "Ground"));        // If it hits something...
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player") && activity != EnemyState.DISTRACTED)
            {
                activity = EnemyState.WATCHING_PALYER;
                lastPhit = hit.collider.gameObject.GetComponent<OthersFearPlayer>();
                lastPhit.scaredOf = this;
                if (bullyText)
                    bullyText.gameObject.SetActive(true);
            }
            else
            {
                if (bullyText)
                    bullyText.gameObject.SetActive(false);
                if (lastPhit != null)
                {
                    lastPhit.scaredOf = null;
                    lastPhit = null;
                }
            }
            return hit.distance / fov.pointLightOuterRadius;
        }
        return 0;
    }

    public void Distracted(bool is_distracted)
    {
        if (is_distracted)
        {
            activity = EnemyState.DISTRACTED;
            fov.color = Color.white;
            if (lastPhit != null)
            {
                lastPhit.scaredOf = null;
                lastPhit = null;
            }
        }
        else
        {
            activity = EnemyState.PATROLLING;
            fov.color = Color.red;
        }
    }

    public EnemyState GetState()
    {
        return activity;
    }

    public float GetSpeed()
    {
        return rb.velocity.x;
    }

    public Vector3 GetDistractPos()
    {
        return transform.position + new Vector3(distractedPos.x * transform.localScale.x, distractedPos.y * transform.localScale.y, 0);
    }
}
