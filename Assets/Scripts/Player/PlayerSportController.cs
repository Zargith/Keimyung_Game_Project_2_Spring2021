using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSportController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask layerForCheck;
    bool isded = false;

    public float jumpForce = 5.0f;

    private void OnEnable()
    {
        SportGameOver.OnGameOver += death;
    }

    private void OnDisable()
    {
        SportGameOver.OnGameOver -= death;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isRunning", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isded)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, layerForCheck);
            GetComponent<Animator>().SetBool("isGrounded", isGrounded);
            if (Input.GetButton("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    void death()
    {
        isded = true;
        GetComponent<Animator>().SetBool("isRunning", false);
    }
}
