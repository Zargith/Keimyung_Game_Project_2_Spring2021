using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirorLevelMoveClone : MonoBehaviour
{
	[SerializeField] GameObject player;
	Animator _anim;
	Vector3 advance = Vector3.zero;
	bool hasJumped = false;
	public bool isClimbing = false;

	void Start()
	{
		_anim = GetComponent<Animator>();
	}


	void Update()
	{
		advance = player.GetComponent<MirorLevelMove>().GetAdvance();
		bool jump = player.GetComponent<MirorLevelMove>().GetJump();
		_anim.SetBool("isGrounded", player.GetComponent<MirorLevelMove>().isGrounded());
		_anim.SetFloat("yVelocity", player.GetComponent<Rigidbody2D>().velocity.y);

		if (jump && !hasJumped) {
			hasJumped = true;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player.GetComponent<MirorLevelMove>().GetJumpHeight()), ForceMode2D.Impulse);
		} else {
			if (!jump)
				hasJumped = false;

			GetComponent<Transform>().position += advance * -1;

			_anim.SetBool("isRunning", advance.x != 0);

		}
	}
}
