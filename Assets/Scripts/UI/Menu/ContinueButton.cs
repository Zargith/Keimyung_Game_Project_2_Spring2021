using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
	[SerializeField] Button ButtonToEnable;
	[SerializeField] Color enabledColor;
	[SerializeField] Color disabledColor;


	void Start()
	{
		Text buttonsText = ButtonToEnable.transform.GetChild(0).GetComponent<Text>();
		if (checkIfTutoDone()) {
			ButtonToEnable.interactable = true;
			buttonsText.color = enabledColor;
		} else {
			ButtonToEnable.interactable = false;
			buttonsText.color = disabledColor;
		}
	}

	bool checkIfTutoDone()
	{
		if (PlayerPrefs.GetInt("DidTuto", 0) != 0)
			return true;
		return false;
	}
}
