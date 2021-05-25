using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperButtonOpenDoor : MonoBehaviour
{
	[SerializeField] GameObject btn1;
	[SerializeField] GameObject btn2;
	public GameObject doorToOpen;
	float time = 0f;

	void Start() {/*only here to have an enablable script*/}

	void Update()
	{
		time += Time.deltaTime;
		// le bouton peut etre active et on saute dessus
		if (btn1.activeSelf && btn1.GetComponent<ButtonColision>().GetCollideState()) {
			btn1.SetActive(false); // on cache le bouton
			btn2.SetActive(true); // pour monte qu'il est active
			time = 0f;
		} else if (time >= 0.5f && btn2.activeSelf) {
			if (!btn2.GetComponent<ButtonColision>().GetCollideState()) {
				btn1.SetActive(true);
				btn2.SetActive(false);
			}
			if (doorToOpen.activeSelf)
				doorToOpen.SetActive(false);
		}
	}
}
