using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterResetGame : MonoBehaviour
{
	[SerializeField] SelfFearGameManager selfFearGameManager;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			audioSource.Play();
			selfFearGameManager.resetGame();
		}
	}
}
