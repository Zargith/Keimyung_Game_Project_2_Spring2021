using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersFearPlayer : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpStrenght = 2.0f;
    Rigidbody2D rb;
    Animator anim;
    bool groundedPlayer;
    AudioSource audioSource;
    [SerializeField] AudioClip footsteps;
    [SerializeField] AudioClip jump;
    public List<OthersFear_Item.EnumOthersFearItemType> inventory;
    public OthersFearEnemy scaredOf;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footsteps;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = IsGrounded();
        anim.SetBool("isGrounded", groundedPlayer);
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (scaredOf != null)
        {
            float xdir = transform.position.x - scaredOf.transform.position.x;
            float sp = Mathf.Max(scaredOf.GetSpeed(), 1.0f);
            rb.velocity = new Vector2(sp * (xdir < 0 ? -1.1f : 1.1f), rb.velocity.y);
        }
        else if (groundedPlayer)
        {
            if (Input.GetButton("Jump") && rb.velocity.y == 0)
            {
                rb.AddForce(new Vector2(0, jumpStrenght), ForceMode2D.Impulse);
                audioSource.PlayOneShot(jump);
                audioSource.volume = 0.05f;
            }
            else
            {
                Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
                rb.velocity = new Vector2(move.x * playerSpeed, rb.velocity.y);
            }
        }
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(rb.velocity.x) == 0 || !IsGrounded())
        {
            audioSource.clip = null;
        }
        else
        {
            audioSource.clip = footsteps;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.volume = 0.25f;
            }
        }

        if (Mathf.Abs(rb.velocity.x) == 0)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
        else if (Mathf.Abs(rb.velocity.x) <= 1.5f)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
    }

    public LayerMask groundLayer;
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.05f;

        Debug.DrawRay(position + new Vector2(2.0f * transform.localScale.x, 0), new Vector2(0, -distance), Color.green);
        Debug.DrawRay(position - new Vector2(1.5f * transform.localScale.x, 0), new Vector2(0, -distance), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position + new Vector2(2.0f * transform.localScale.x, 0), direction, distance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(position - new Vector2(1.5f * transform.localScale.x, 0), direction, distance, groundLayer);
        return hit.collider != null || hit2.collider != null;
    }
}
