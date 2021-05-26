using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayIfPlayerIsInZone : MonoBehaviour
{
	[SerializeField] GameObject elemToDisplay;
	[SerializeField] TextMesh playerThoughts;
	bool isPlayerStayingInZone = false;

	void Start() {/*only here to have an enablable script*/}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && (playerThoughts == null || playerThoughts.text == ""))
			elemToDisplay.SetActive(true);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			isPlayerStayingInZone = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			elemToDisplay.SetActive(false);
			isPlayerStayingInZone = false;
		}
	}

	void Update()
	{
		if (isPlayerStayingInZone && (playerThoughts == null || playerThoughts.text == ""))
			elemToDisplay.SetActive(true);
	}
}