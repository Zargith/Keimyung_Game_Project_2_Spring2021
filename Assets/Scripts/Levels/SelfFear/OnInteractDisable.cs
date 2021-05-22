using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractDisable : MonoBehaviour
{

	[SerializeField] GameObject elemDisplayed;
	[SerializeField] public GameObject elemToDisable;
	[SerializeField] public DisplayIfPlayerIsInZoneButOnlyOneActivation displayIfPlayerIsInZoneButOnlyOneActivationScript;

	[SerializeField] SpriteRenderer _renderer;

	void Start() {/*only here to have an enablable script*/}

	void Update()
	{
		if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
			elemToDisable.SetActive(false);
			_renderer.flipY = true;
			displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = true;
		}
	}
}
