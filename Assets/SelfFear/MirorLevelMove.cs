using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirorLevelMove : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 advance = Vector3.zero;
    private bool jump = false;

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
            jump = true;
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        } else {
            var horizontalInput = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(horizontalInput, 0, 0);

            advance = move * Time.deltaTime * playerSpeed;
            transform.position += advance;

            anim.SetBool("isRunning", move.x != 0);
        }
    }

    public bool GetJump()
    {
        return jump;
    }

    public bool GetJumpAnim()
    {
        return groundedPlayer;
    }

    public float GetJumpHeight()
    {
        return jumpHeight;
    }
    public Vector3 GetAdvance()
    {
        return advance;
    }

     bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
            return true;
        jump = false;
        return false;
    }
}
