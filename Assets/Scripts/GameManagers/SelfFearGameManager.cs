using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfFearGameManager : MonoBehaviour
{
	[SerializeField] GameObject playerGO;
	[SerializeField] Vector2 playerRespawnPoint;
	[SerializeField] GameObject playerCloneGO;
	[SerializeField] Vector2 playerCloneRespawnPoint;
	[SerializeField] GameObject[] switches;
	[SerializeField] GameObject[] buttonsThatOpenDoors;

	void Start()
	{
	}

	void Update()
	{
	}

	public void resetGame()
	{
		// Reset player and clone to respawn position
		playerGO.transform.position = playerRespawnPoint;
		playerCloneGO.transform.position = playerCloneRespawnPoint;

		// Reset switches
		for (int i = 0; i < switches.Length; i++) {
			SpriteRenderer _renderer;
			if (switches[i].TryGetComponent<SpriteRenderer>(out _renderer))
				_renderer.flipY = false;

			// Close doors opened by switchs
			OnInteractDisable onInteractDisableScript;
			if (switches[i].transform.GetChild(0).TryGetComponent<OnInteractDisable>(out onInteractDisableScript)) {
				onInteractDisableScript.elemToDisable.SetActive(true);
				onInteractDisableScript.displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = false;
			}
		}

		// Close doors opened by buttons
		for (int i = 0; i < buttonsThatOpenDoors.Length; i++) {
			JumperButtonOpenDoor jumperButtonOpenDoorScript = buttonsThatOpenDoors[i].GetComponent<JumperButtonOpenDoor>();
			jumperButtonOpenDoorScript.doorToOpen.SetActive(true);
		}

	}
}
