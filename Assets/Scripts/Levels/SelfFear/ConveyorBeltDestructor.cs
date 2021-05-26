using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltDestructor : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("ConveyorBeltBlock"))
			Destroy(other.gameObject);
	}
}
