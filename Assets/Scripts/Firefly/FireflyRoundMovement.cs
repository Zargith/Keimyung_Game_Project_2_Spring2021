using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyRoundMovement : MonoBehaviour
{
	Vector3 startPos;

	[SerializeField] float speed = 1;
	[SerializeField] float xScale = 1;
	[SerializeField] float yScale = 1;

	void Start () {
		startPos = transform.localPosition;
	}

	void Update ()
	{
		transform.localPosition = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
	}
}
