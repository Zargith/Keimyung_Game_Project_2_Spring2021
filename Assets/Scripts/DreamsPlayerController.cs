using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamsPlayerController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public bool canJump = true;
    public float jumpHeight = 1.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundedPlayer = IsGrounded();
        anim.SetBool("isGrounded", groundedPlayer);
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (Input.GetButtonDown("Jump") && canJump && groundedPlayer) {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * Time.deltaTime * playerSpeed;
        if (move.x == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);
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
