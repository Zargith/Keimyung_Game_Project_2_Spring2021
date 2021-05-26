using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractStopConveyorBelt : MonoBehaviour
{
	[SerializeField] GameObject elemDisplayed;
	[SerializeField] GameObject ConveyerBelt;
	[SerializeField] SpriteRenderer _renderer;
	GameObject spawner;
	GameObject chainSaw;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		int children = ConveyerBelt.transform.childCount;
		for (int i = 0; i < children; i++) {
			GameObject child = ConveyerBelt.transform.GetChild(i).gameObject;
			if (child.name == "Spawner")
				spawner = child;
			else if (child.name == "ChainSaw")
				chainSaw = child;
		}
	}

	void Update()
	{
		if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
			_renderer.flipY = true;
			chainSaw.SetActive(false);
			spawner.GetComponent<ConveyorBeltSpawner>().enabled = false;
			int spawnerChildren = spawner.transform.childCount;
			for (int i = 0; i < spawnerChildren; i++)
				spawner.transform.GetChild(i).GetComponent<ConveyorBeltMovingBlocks>().enabled = false;
			audioSource.Play();
		}
	}

	public void Reset()
	{
		_renderer.flipY = false;
		chainSaw.SetActive(true);
		spawner.GetComponent<ConveyorBeltSpawner>().enabled = true;
		int spawnerChildren = spawner.transform.childCount;
		for (int i = 0; i < spawnerChildren; i++)
			spawner.transform.GetChild(i).GetComponent<ConveyorBeltMovingBlocks>().enabled = true;
	}
}
