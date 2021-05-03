using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	bool facingRight = true;
	[SerializeField] float moveSpeed = 5.0f;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		anim.SetBool("isGrounded", true);
	}

	void Update() {
		float horizontalInput = Input.GetAxis("Horizontal");
		if (horizontalInput > 0 && !facingRight)
			Flip();
		else if (horizontalInput < 0 && facingRight)
			Flip();

		Vector3 movement = new Vector3(horizontalInput, Input.GetAxis("Vertical"), 0);
		transform.position += movement * Time.deltaTime * moveSpeed;
		if (movement == new Vector3(0, 0, 0))
			anim.SetBool("isWalking", false);
		else
			anim.SetBool("isWalking", true);
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector2 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
}