using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperButtonOpenDoor : MonoBehaviour
{
	[SerializeField] GameObject btn1;
	[SerializeField] GameObject btn2;
	public GameObject doorToOpen;
	public bool doorOpened = false;
	float time = 0f;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}


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
			if (doorToOpen.activeSelf && !doorOpened) {
				doorToOpen.SetActive(false);
				doorOpened = true;
				audioSource.Play();
			}
		}
	}

	public void Reset()
	{
		doorToOpen.SetActive(true);
		doorOpened = false;
	}

}
