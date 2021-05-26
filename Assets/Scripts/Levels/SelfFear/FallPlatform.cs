using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
	Rigidbody2D rb;
	Vector2 startPos;
	[SerializeField] float fallDelay = 0.5f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		startPos = transform.position;
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			Invoke("dropPlatform", fallDelay);
	}

	void dropPlatform()
	{
		rb.isKinematic = false;
	}

	public void Reset()
	{
		this.transform.position = startPos;
		rb.isKinematic = true;
	}
}
