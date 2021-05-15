using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractWithElementChangeTxtDepenedingPlayerPrefs : MonoBehaviour
{
	[SerializeField] GameObject elemToHide;
	[SerializeField] TextMesh textMesh;
	[SerializeField] string playerPrefName;
	[SerializeField] string textIfPlayerPrefFalse;
	List<string> splittedTextIfPlayerPrefFalse;
	bool isTextIfPlayerPrefFalseTooLong = false;
	[SerializeField] string textIfPlayerPrefTrue;
	List<string> splittedTextIfPlayerPrefTrue;
	bool isTextIfPlayerPrefTrueTooLong = false;
	[SerializeField] int maxStringLength = 50;
	[SerializeField] float timeInSecondsDisplayEachTextLine = 2.5f;
	bool textFinishedToDisplay = true;

	void Start() {
		if (textIfPlayerPrefFalse.Length > maxStringLength) {
			isTextIfPlayerPrefFalseTooLong = true;
			splittedTextIfPlayerPrefFalse = splitStringInListOfSizedElements(textIfPlayerPrefFalse);
		}
		if (textIfPlayerPrefTrue.Length > maxStringLength) {
			isTextIfPlayerPrefTrueTooLong = true;
			splittedTextIfPlayerPrefTrue = splitStringInListOfSizedElements(textIfPlayerPrefTrue);
		}
	}

	void debugList(List<string> arr)
	{
		foreach (string arrItem in arr)
			Debug.Log(arrItem);
	}

	List<string> stringArrayToStringList(string[] arr)
	{
		List<string> openItems = new List<string>();

		foreach (string arrItem in arr)
			openItems.Add(arrItem);

		return openItems;
	}

	List<string> splitStringInListOfSizedElements(string longStr)
	{
			List<string> tmp = new List<string>();
			List<string> listStrLineElements = stringArrayToStringList(longStr.Split(' '));

			while (listStrLineElements.Count > 0) {
				string newArrayElem = "";
				string firstElem = listStrLineElements[0];

				if (firstElem.Length >= maxStringLength) {
					newArrayElem += (tmp.Count == 0 ? firstElem : " " + firstElem);
					listStrLineElements.RemoveAt(0);
					continue;
				}

				while (newArrayElem.Length < maxStringLength && listStrLineElements.Count > 0) {
					firstElem = listStrLineElements[0];
					if (newArrayElem.Length + firstElem.Length <= maxStringLength) {
						newArrayElem += (newArrayElem.Length == 0 ? firstElem : " " + firstElem);
						listStrLineElements.RemoveAt(0);
						continue;
					} else
						break;
				}

				tmp.Add(newArrayElem);
			}
		return tmp;
	}

	void Update()
	{
		if (textFinishedToDisplay && elemToHide.activeSelf && Input.GetButtonDown("Interact")) {
			textFinishedToDisplay = false;
			StartCoroutine(displayText());
			elemToHide.SetActive(false);
		}
	}

	IEnumerator displayTextFromListOfStr(List<string> _list)
	{
		for (int i = 0; i < _list.Count; i++) {
			textMesh.text = _list[i];
			yield return new WaitForSeconds(timeInSecondsDisplayEachTextLine);
		}
	}

	IEnumerator displayText()
	{
		yield return new WaitForSeconds(0.5f);

		if (PlayerPrefs.GetInt(playerPrefName, 0) == 0 && isTextIfPlayerPrefFalseTooLong) {
			for (int i = 0; i < splittedTextIfPlayerPrefFalse.Count; i++) {
				textMesh.text = splittedTextIfPlayerPrefFalse[i];
				yield return new WaitForSeconds(timeInSecondsDisplayEachTextLine);
			}
		} else if (PlayerPrefs.GetInt(playerPrefName, 0) == 1 && isTextIfPlayerPrefTrueTooLong) {
			for (int i = 0; i < splittedTextIfPlayerPrefTrue.Count; i++) {
				textMesh.text = splittedTextIfPlayerPrefTrue[i];
				yield return new WaitForSeconds(timeInSecondsDisplayEachTextLine);
			}
		} else {
			textMesh.text = (PlayerPrefs.GetInt(playerPrefName, 0) == 1 ? textIfPlayerPrefTrue : textIfPlayerPrefFalse);
			yield return new WaitForSeconds(timeInSecondsDisplayEachTextLine);
		}

		textMesh.text = "";
		textFinishedToDisplay = true;
	}
}
