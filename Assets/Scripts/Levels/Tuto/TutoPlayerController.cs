using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float playerSpeed = 2.0f;
    public bool canMove = false;
    [SerializeField] bool canJump = false;
    [SerializeField] float jumpHeight = 1.0f;
    Rigidbody2D rb;
    Vector3 playerVelocity;
    bool groundedPlayer;
    AudioSource audioSource;
    [SerializeField] AudioClip forestFootsteps;
    [SerializeField] AudioClip otherFootsteps;
    [SerializeField] float xPositionToChangeFootstepAudioClip = 0f;
    private bool beyondXPositionToChangeFootstepAudioClip = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = forestFootsteps;
        audioSource.Play();
        audioSource.Pause();
    }

    void Update()
    {
        groundedPlayer = IsGrounded();
        anim.SetBool("isGrounded", groundedPlayer);
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (!canMove)
            return;

        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (Input.GetButtonDown("Jump") && canJump && groundedPlayer) {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            return;
        }

        if (transform.position.x > xPositionToChangeFootstepAudioClip && !beyondXPositionToChangeFootstepAudioClip) {
            audioSource.clip = otherFootsteps;
            audioSource.volume = 1;
            audioSource.Play();
            beyondXPositionToChangeFootstepAudioClip = true;
        } else if (transform.position.x < xPositionToChangeFootstepAudioClip && beyondXPositionToChangeFootstepAudioClip) {
            audioSource.clip = forestFootsteps;
            audioSource.volume = 0.25f;
            audioSource.Play();
            beyondXPositionToChangeFootstepAudioClip = false;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * Time.deltaTime * playerSpeed;
        if (move.x == 0) {
            anim.SetBool("isRunning", false);
            audioSource.Pause();
        } else {
            anim.SetBool("isRunning", true);
            audioSource.UnPause();
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
