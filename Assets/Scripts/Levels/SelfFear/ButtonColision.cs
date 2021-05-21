using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColision : MonoBehaviour
{
	private bool collide = false;

	void Start() {/*only here to have an enablable script*/}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			collide = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			collide = false;
	}

	public bool GetCollideState()
	{
		return collide;
	}
}
