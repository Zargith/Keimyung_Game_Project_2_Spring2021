using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfRotateZ : MonoBehaviour
{
	[SerializeField] float rotationSpeed = 1.0f;

	void Update()
	{
		if (transform.rotation.z >= 360)
			transform.rotation = Quaternion.identity;

		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = rotationVector.z + rotationSpeed;
		transform.rotation = Quaternion.Euler(rotationVector);
	}
}
