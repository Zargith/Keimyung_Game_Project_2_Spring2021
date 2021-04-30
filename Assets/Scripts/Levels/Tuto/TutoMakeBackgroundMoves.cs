using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMakeBackgroundMoves : MonoBehaviour
{
	[SerializeField] float moveSpeed = 0.01f;
	[SerializeField] float limitXmin = -8.9f;
	[SerializeField] float limitXmax = 80f;
	[SerializeField] GameObject player;
	Animator playerAnimator;
	FlipPlayer flipPlayerScript;
	int playerOrientation = 1;

	void Start() {
		playerAnimator = player.GetComponent<Animator>();
		flipPlayerScript = player.GetComponent<FlipPlayer>();
	}

	void Update()
	{
		Vector2 playerPostion = player.transform.position;
		if (isPlayerMoving() && playerPostion.x > limitXmin && playerPostion.x < limitXmax) {
			foreach (Transform child in transform) {
				float offset = playerOrientation * moveSpeed;
				child.localPosition = new Vector2(child.localPosition.x + (getPlayerOrintation() * moveSpeed), child.localPosition.y);
			}
		}
	}

	int getPlayerOrintation() {
		return (flipPlayerScript.isPlayerFacingRight() ? -1 : 1);
	}

	bool isPlayerMoving()
	{
		if (playerAnimator.GetBool("isRunning") || playerAnimator.GetBool("isWalking"))
			return true;
		return false;
	}
}
