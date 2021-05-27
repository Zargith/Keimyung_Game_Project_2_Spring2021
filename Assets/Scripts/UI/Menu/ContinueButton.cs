using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
	[SerializeField] Button ButtonToEnable;

	void Start()
	{
		if (checkIfTutoDone())
			ButtonToEnable.interactable = true;
	}

	bool checkIfTutoDone()
	{
		if (PlayerPrefs.GetInt("DidTuto", 0) != 0)
			return true;
		return false;
	}
}
