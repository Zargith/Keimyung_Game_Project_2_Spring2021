using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundLadder : MonoBehaviour
{
	[SerializeField] float localXStartPos;
	[SerializeField] float offsetXPosition = 1f;
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] bool movingRight = true;

	void Update()
	{
		if (movingRight) {
			if (transform.localPosition.x > localXStartPos + offsetXPosition)
				movingRight = false;
			else
				transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		} else {
			if (transform.localPosition.x < localXStartPos - offsetXPosition)
				movingRight = true;
			else
				transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

		}
	}
}
