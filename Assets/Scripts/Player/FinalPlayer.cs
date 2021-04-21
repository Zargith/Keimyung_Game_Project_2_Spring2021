using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FlipPlayer))]
public class FinalPlayer : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float playerClimbSpeed = 1.5f;
    Rigidbody2D rb;
    Animator anim;
    bool groundedPlayer;
    bool climbing;
    AudioSource audioSource;
    [SerializeField] AudioClip forestFootsteps;
    [SerializeField] AudioClip otherFootsteps;
    [SerializeField] float xPositionToChangeFootstepAudioClip = 0f;
    private bool beyondXPositionToChangeFootstepAudioClip = false;

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

        if (Mathf.Abs(transform.position.x) >= 65)
        {
            transform.position = new Vector3(
                (transform.position.x > 0 ? -120 : 120) + transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        if (transform.position.x > xPositionToChangeFootstepAudioClip && !beyondXPositionToChangeFootstepAudioClip)
        {
            audioSource.clip = otherFootsteps;
            audioSource.volume = 1;
            audioSource.Play();
            beyondXPositionToChangeFootstepAudioClip = true;
        }
        else if (transform.position.x < xPositionToChangeFootstepAudioClip && beyondXPositionToChangeFootstepAudioClip)
        {
            audioSource.clip = forestFootsteps;
            audioSource.volume = 0.25f;
            audioSource.Play();
            beyondXPositionToChangeFootstepAudioClip = false;
        }

        if (groundedPlayer)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * Time.deltaTime * playerSpeed;
            if (move.x == 0)
            {
                anim.SetBool("isRunning", false);
                audioSource.Pause();
            }
            else
            {
                anim.SetBool("isRunning", true);
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

    public void Climb(bool doClimb)
    {
        climbing = doClimb;
        if (doClimb)
        {
            rb.velocity = new Vector2(0, playerClimbSpeed);
            rb.gravityScale = 0;
            GetComponent<FlipPlayer>().enabled = false;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 1;
            GetComponent<FlipPlayer>().enabled = true;
        }
    }
}
