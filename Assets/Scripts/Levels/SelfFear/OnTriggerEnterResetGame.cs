using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterResetGame : MonoBehaviour
{
	[SerializeField] SelfFearGameManager selfFearGameManager;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player"))
			selfFearGameManager.resetGame();
	}
}
