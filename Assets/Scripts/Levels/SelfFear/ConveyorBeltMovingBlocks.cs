using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMovingBlocks : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1.0f;

	void Update()
	{
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
	}
}
