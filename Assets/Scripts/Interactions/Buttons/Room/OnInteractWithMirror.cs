using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractWithMirror : MonoBehaviour
{
	[SerializeField] GameObject elemToHide;
	[SerializeField] TextMesh textMesh;
	bool textFinishedToDisplay = true;

	void Update()
	{
		if (textFinishedToDisplay && elemToHide.activeSelf && Input.GetButtonDown("Interact")) {
			textFinishedToDisplay = false;
			StartCoroutine(displayText());
			elemToHide.SetActive(false);
		}
	}

	IEnumerator displayText()
	{
		yield return new WaitForSeconds(0.5f);
		textMesh.text = "I don't want to see myself in the mirror...";

		yield return new WaitForSeconds(5.0f);
		textMesh.text = "";

		textFinishedToDisplay = true;
	}
}
