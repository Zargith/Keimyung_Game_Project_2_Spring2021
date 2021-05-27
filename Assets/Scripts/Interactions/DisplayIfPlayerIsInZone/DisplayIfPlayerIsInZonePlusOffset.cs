using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIfPlayerIsInZonePlusOffset : MonoBehaviour
{
	[SerializeField] GameObject elemToDisplay;
	[SerializeField] Vector2 offset;

	void Start() {/*only here to have an enablable script*/}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) {
			Vector2 playerPos = other.gameObject.transform.position;
			Vector2 thisPos = this.transform.position;
			if (playerPos.x >= thisPos.x + offset.x && playerPos.y >= thisPos.y + offset.y)
				elemToDisplay.SetActive(true);
		}
	}

	// void OnTriggerStay2D(Collider2D other)
	// {
	// 	if (other.gameObject.CompareTag("Player"))
	// 		elemToDisplay.SetActive(true);
	// }

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player"))
			elemToDisplay.SetActive(false);
	}
}
