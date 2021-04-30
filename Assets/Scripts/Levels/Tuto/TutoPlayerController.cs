using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPlayerController : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] float playerRunSpeed = 2.5f;
	[SerializeField] float playerWalkSpeed = 1.5f;
	float playerCurrentSpeed = 0f;
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
	bool beyondXPositionToChangeFootstepAudioClip = false;
	[SerializeField] bool canRun = true;
	[SerializeField] float xPositionToStopRun = 28f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = forestFootsteps;
		audioSource.Play();
		audioSource.Pause();
		playerCurrentSpeed = (canRun ? playerRunSpeed : playerWalkSpeed);
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

		if (transform.position.x >= xPositionToStopRun && canRun)
			canRun = false;

		if (!canRun && playerCurrentSpeed > playerWalkSpeed)
			playerCurrentSpeed -= 0.1f;
		if (canRun && playerCurrentSpeed < playerRunSpeed)
			playerCurrentSpeed += 0.1f;

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
		transform.position += move * Time.deltaTime * playerCurrentSpeed;
		if (move.x == 0) {
			anim.SetBool("isRunning", false);
			anim.SetBool("isWalking", false);
			audioSource.Pause();
		} else {
			if (canRun)
				anim.SetBool("isRunning", true);
			else
				anim.SetBool("isWalking", true);
			audioSource.UnPause();
		}
	}


	[SerializeField] LayerMask groundLayer;
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
