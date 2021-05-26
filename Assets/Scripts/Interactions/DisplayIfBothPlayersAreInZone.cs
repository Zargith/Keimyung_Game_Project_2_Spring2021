using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIfBothPlayersAreInZone : MonoBehaviour
{
	[SerializeField] GameObject elemToDisplay;
	[SerializeField] string playerObjectName;
	[SerializeField] string playerCloneObjectName;
	bool isPlayerStayingInZone = false;
	bool isPlayerCloneStayingInZone = false;

	void Start() {/*only here to have an enablable script*/}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) {
			if (other.gameObject.name == playerObjectName)
				isPlayerStayingInZone = true;
			else if (other.gameObject.name == playerCloneObjectName)
				isPlayerCloneStayingInZone = true;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) {
			if (other.gameObject.name == playerObjectName)
				isPlayerStayingInZone = true;
			else if (other.gameObject.name == playerCloneObjectName)
				isPlayerCloneStayingInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			if (other.gameObject.name == playerObjectName)
				isPlayerStayingInZone = false;
			else if (other.gameObject.name == playerCloneObjectName)
				isPlayerCloneStayingInZone = false;
		}
	}

	void Update()
	{
		if (isPlayerStayingInZone && isPlayerCloneStayingInZone)
			elemToDisplay.SetActive(true);
		else
			elemToDisplay.SetActive(false);
	}
}
