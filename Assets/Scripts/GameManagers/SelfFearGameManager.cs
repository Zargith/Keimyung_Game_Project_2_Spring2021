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
	[SerializeField] GameObject[] buttons;
	[SerializeField] GameObject fallingPlatforms;

	void Start() {/*only here to have an enablable script*/}


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
			// Enable element disiabled by switch
			OnInteractDisable onInteractDisableScript;
			if (switches[i].transform.GetChild(0).TryGetComponent<OnInteractDisable>(out onInteractDisableScript))
				onInteractDisableScript.Reset();

			// Enable elements disiabled opened by switch
			OnInteractDisableMany onInteractDisableManyScript;
			if (switches[i].transform.GetChild(0).TryGetComponent<OnInteractDisableMany>(out onInteractDisableManyScript))
				onInteractDisableManyScript.Reset();

			// Disiable element enabled by switch
			OnInteractEnable onInteractEnableScript;
			if (switches[i].transform.GetChild(0).TryGetComponent<OnInteractEnable>(out onInteractEnableScript))
				onInteractEnableScript.Reset();


			// Restart conveyor belt stoped by switch
			OnInteractStopConveyorBelt onInteractStopConveyorBeltScript;
			if (switches[i].transform.GetChild(0).TryGetComponent<OnInteractStopConveyorBelt>(out onInteractStopConveyorBeltScript))
				onInteractStopConveyorBeltScript.Reset();
		}

		for (int i = 0; i < buttons.Length; i++) {
			// Close door opened by button
			JumperButtonOpenDoor jumperButtonOpenDoorScript = buttons[i].GetComponent<JumperButtonOpenDoor>();
			jumperButtonOpenDoorScript.Reset();
		}

		int nbFallingPlatforms = fallingPlatforms.transform.childCount;
		for (int i = 0; i < nbFallingPlatforms; i++) {
			fallingPlatforms.transform.GetChild(i).GetComponent<FallPlatform>().Reset();
		}
	}
}
