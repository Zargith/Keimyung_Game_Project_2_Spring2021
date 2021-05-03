using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FlipPlayer))]
public class FinalPlayer : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    Rigidbody2D rb;
    Animator anim;
    bool groundedPlayer;
    AudioSource audioSource;
    [SerializeField] AudioClip forestFootsteps;
    [SerializeField] AudioClip otherFootsteps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = forestFootsteps;
    }

    // Update is called once per frame
    void Update()
    {

        groundedPlayer = IsGrounded();
        anim.SetBool("isGrounded", groundedPlayer);
        anim.SetFloat("yVelocity", rb.velocity.y);


        if (groundedPlayer && rb.velocity.y < 0)
            rb.velocity = new Vector2(rb.velocity.x, 0f);

        if (transform.position.x > 12)
        {
            transform.position += new Vector3(1f, 0, 0) * Time.deltaTime * playerSpeed;
            if (playerSpeed <= 1.5)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
            }
            return;
        }

        if (groundedPlayer)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * Time.deltaTime * playerSpeed;
            if (move.x == 0)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                audioSource.Pause();
            }
            else
            {
                if (playerSpeed <= 1.5)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", true);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                }
                audioSource.UnPause();
            }
        }
    }

    public LayerMask groundLayer;
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        Debug.DrawRay(position, new Vector2(0, -distance), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
            return true;
        return false;
    }
}
