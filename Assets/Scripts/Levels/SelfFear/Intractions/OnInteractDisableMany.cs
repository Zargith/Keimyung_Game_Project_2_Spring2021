using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractDisableMany : MonoBehaviour
{
	[SerializeField] GameObject elemDisplayed;
	public GameObject[] elemsToDisable;
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
			for (int i = 0; i < elemsToDisable.Length; i++)
				elemsToDisable[i].SetActive(false);
			_renderer.flipY = true;
			displayIfPlayerIsInZoneButOnlyOneActivationScript.activatedOnce = true;
			audioSource.Play();
		}
	}
}
