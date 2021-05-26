using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionMoving : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1.0f;
	[SerializeField] bool goingRight = true;

	void Update()
	{
		transform.Translate((goingRight ? Vector2.right : Vector2.left) * moveSpeed * Time.deltaTime);
	}
}
