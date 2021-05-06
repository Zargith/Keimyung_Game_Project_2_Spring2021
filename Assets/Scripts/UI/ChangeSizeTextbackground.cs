using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeTextbackground : MonoBehaviour
{
	[SerializeField] TextMesh textMesh;
	[SerializeField] float defaultCharSize = 1.0f;
	[SerializeField] float charSizeLittle = 1.35f;
	[SerializeField] string littleChars = "";
	[SerializeField] float charSizeMedium = 1.6f;
	[SerializeField] string mediumChars = "";
	[SerializeField] float charSizeBig = 1.85f;
	[SerializeField] string bigChars = "";
	bool hadText = false;

	void Start()
	{
		transform.localScale = new Vector3(0, 5, 1);
		littleChars = littleChars.ToLower();
		mediumChars = mediumChars.ToLower();
		bigChars = bigChars.ToLower();
	}

	void Update()
	{
		if (textMesh.text != "") {
			float size = calculateCharsSize(textMesh.text.ToLower());
			transform.localScale = new Vector3(size, 5, 1);
			hadText = true;
		} else if (hadText) {
			transform.localScale = new Vector3(0 , 5, 1);
			hadText = true;
		}
	}

	float calculateCharsSize(string str)
	{
		float res = 0.0f;

		for (int i = 0; i < str.Length; i++) {
			if (isCharSized(str[i], littleChars)) {
				res += charSizeLittle;
				continue;
			} else if (isCharSized(str[i], mediumChars)) {
				res += charSizeMedium;
				continue;
			} else if (isCharSized(str[i], bigChars)) {
				res += charSizeBig;
				continue;
			} else {
				Debug.Log("'" + str[i].ToString() + "'");
				res += defaultCharSize;
			}
		}

		return res;
	}

	bool isCharSized(char c, string sizedChars)
	{
		for (int i = 0; i < sizedChars.Length; i++)
			if (c == sizedChars[i])
				return true;
		return false;
	}

}
