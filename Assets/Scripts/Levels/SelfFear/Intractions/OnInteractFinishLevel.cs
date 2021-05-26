using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnInteractFinishLevel : MonoBehaviour
{
	[SerializeField] GameObject elemDisplayed;
	[SerializeField] string sceneName;
	[SerializeField] string levelPlayerPrefName;

	void Update()
	{
		if (elemDisplayed.activeSelf && Input.GetButtonDown("Interact")) {
			SceneManager.LoadScene(sceneName);
			PlayerPrefs.SetInt(levelPlayerPrefName, 1);
		}
	}
}
