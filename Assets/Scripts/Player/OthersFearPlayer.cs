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
    public List<OthersFear_Item> inventory;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footsteps;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = IsGrounded();
        anim.SetBool("isGrounded", groundedPlayer);
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (groundedPlayer)
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(new Vector2(0, jumpStrenght), ForceMode2D.Impulse);
            }
            else
            {
                Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
                rb.velocity = new Vector2(move.x * playerSpeed, rb.velocity.y);
                Move();
            }
        }
    }

    private void Move()
    {
        if (Mathf.Abs(rb.velocity.x) == 0)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            audioSource.Pause();
        }
        else if (Mathf.Abs(rb.velocity.x) <= 1.5f)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            audioSource.UnPause();
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
            audioSource.UnPause();
        }
    }

    public LayerMask groundLayer;
    bool IsGrounded()
    {
        return rb.velocity.y == 0;
        /*
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        Debug.DrawRay(position, new Vector2(0, -distance), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
            return true;
        return false;
        */
    }
}
