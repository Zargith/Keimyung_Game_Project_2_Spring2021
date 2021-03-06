using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractDisable : MonoBehaviour
{
	[SerializeField] GameObject elemDisplayed;
	public GameObject elemToDisable;
	public DisplayIfPlayerIsInZoneButOnlyOneActivation displayIfPlayerIsInZoneButOnlyOneActivationScript;
	[SerializeField] SpriteRenderer _renderer;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
			elemToDisable.SetActive(false);
			_renderer.flipY = true;
			displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = true;
			audioSource.Play();
		}
	}

	public void Reset()
	{
		elemToDisable.SetActive(true);
		_renderer.flipY = false;
		displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = false;
	}
}
