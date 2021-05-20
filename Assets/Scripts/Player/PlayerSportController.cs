using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSportController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheck1;
    [SerializeField] LayerMask layerForCheck;
    bool isded = false;
    Animator anim;

    public float jumpForce = 5.0f;

    bool deadByRock;
    Vector3 actpos;
    Vector3 aaa;
    float elapsedtime;

    AudioSource aud;

    private void OnEnable()
    {
        SportGameOver.OnGameOver += stop;
        SportGameOver.OnRestart += revive;
    }

    private void OnDisable()
    {
        SportGameOver.OnGameOver -= stop;
        SportGameOver.OnRestart -= revive;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        anim.SetBool("isGrounded", true);
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isded)
        {
            bool wg = isGrounded;
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, layerForCheck) || Physics2D.OverlapCircle(groundCheck1.position, 0.1f, layerForCheck);
            if ((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
            if (wg && !isGrounded)
            {
                aud.Stop();
            }
            if (!wg && isGrounded)
            {
                aud.Play();
            }
        } else if (deadByRock)
        {
            elapsedtime += Time.deltaTime;
            transform.position = Vector3.Lerp(actpos, aaa, elapsedtime);
        }
    }

    public void stop()
    {
        isded = true;
        anim.SetBool("isRunning", false);
    }

    public void dieByRock()
    {
        isded = true;
        anim.SetBool("isRunning", false);
        actpos = transform.position;
        aaa = new Vector3(-11, actpos.y, 0);
        elapsedtime = 0;
        deadByRock = true;
    }

    public void revive()
    {
        isded = false;
        transform.position = new Vector3(-3.3f, -2.4f, 0);
        anim.SetBool("isRunning", true);
        deadByRock = false;
    }

    public void shake()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * 6.5f, ForceMode2D.Impulse);
        }
    }

    public void addForce(float force)
    {
        jumpForce += force;
    }
}
