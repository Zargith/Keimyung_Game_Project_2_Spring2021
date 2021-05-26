using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirorLevelMoveClone2 : MonoBehaviour
{
	Animator anim;
	Rigidbody2D rb;
	Vector3 playerVelocity;
	bool groundedPlayer;
	[SerializeField] float playerSpeed = 2.0f;
	[SerializeField] bool canJump = true;
	[SerializeField] float jumpHeight = 1.0f;
	[SerializeField] LayerMask groundLayer;
	Vector3 advance = Vector3.zero;
	bool jump = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		groundedPlayer = CheckGrounded();
		anim.SetBool("isGrounded", groundedPlayer);
		anim.SetFloat("yVelocity", rb.velocity.y);

		if (groundedPlayer && playerVelocity.y < 0)
			playerVelocity.y = 0f;

		if (Input.GetButtonDown("Jump") && canJump && groundedPlayer) {
			jump = true;
			rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
		} else {
			var horizontalInput = Input.GetAxis("Horizontal");
			Vector3 move = new Vector3(-horizontalInput, 0, 0);

			advance = move * Time.deltaTime * playerSpeed;
			transform.position += advance;

			anim.SetBool("isRunning", move.x != 0);
		}
	}

	public bool GetJump()
	{
		return jump;
	}

	public bool isGrounded()
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

	bool CheckGrounded()
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
