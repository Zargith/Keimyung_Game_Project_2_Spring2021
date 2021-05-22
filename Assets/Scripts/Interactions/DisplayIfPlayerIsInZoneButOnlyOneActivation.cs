using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIfPlayerIsInZoneButOnlyOneActivation : MonoBehaviour
{
	[SerializeField] GameObject elemToDisplay;
	public bool activatedOnce = false;

	void Start() {/*only here to have an enablable script*/}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && !activatedOnce)
			elemToDisplay.SetActive(true);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && !activatedOnce)
			elemToDisplay.SetActive(false);
	}

	void Update()
	{
		if (elemToDisplay.activeSelf && activatedOnce)
			elemToDisplay.SetActive(false);
	}
}
