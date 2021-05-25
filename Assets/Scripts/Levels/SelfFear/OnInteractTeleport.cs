using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractTeleport : MonoBehaviour
{
	[SerializeField] GameObject elemToTeleport;
	[SerializeField] GameObject elemToDisplay;
	[SerializeField] Vector2 teleportPoint;

	void Update()
	{
		if (elemToDisplay.activeSelf && Input.GetButtonDown("Interact"))
			elemToTeleport.transform.position = teleportPoint;
	}
}
