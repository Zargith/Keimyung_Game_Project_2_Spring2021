using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayIfPlayerIsInZone : MonoBehaviour
{
	[SerializeField] GameObject elemToDisplay;

	private void Start() {/*only here to have an enablable script*/}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			elemToDisplay.SetActive(true);
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player"))
			elemToDisplay.SetActive(false);
	}
}
