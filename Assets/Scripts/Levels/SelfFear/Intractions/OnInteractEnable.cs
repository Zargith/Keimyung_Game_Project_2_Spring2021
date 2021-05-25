using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractEnable : MonoBehaviour
{
	[SerializeField] GameObject elemDisplayed;
	public GameObject elemToEnable;
	public DisplayIfPlayerIsInZoneButOnlyOneActivation displayIfPlayerIsInZoneButOnlyOneActivationScript;
	[SerializeField] SpriteRenderer _renderer;

	void Start() {/*only here to have an enablable script*/}

	void Update()
	{
		if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
			elemToEnable.SetActive(true);
			_renderer.flipY = true;
			displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = true;
		}
	}
}
