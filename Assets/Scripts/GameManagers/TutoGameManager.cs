using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGameManager : MonoBehaviour
{
	[SerializeField] GameObject firefly;
	[SerializeField] GameObject player;
	[SerializeField] AudioSource cameraAudioSource;
	TutoPlayerController tutoPlayerController;
	FlipPlayer playerFlipPlayerScript;
	[SerializeField] GameObject intaractibleObject;
	[SerializeField] TextMesh textMesh;
	bool textFinishedToDisplay = false;
	bool waitInteractonWithBed = false;
	[SerializeField] Vector2 respawnPosition;
	public bool pause = false;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject tmpPauseText;

	void Start()
	{
		firefly.SetActive(false);
		tutoPlayerController = player.GetComponent<TutoPlayerController>();
		playerFlipPlayerScript = tutoPlayerController.GetComponent<FlipPlayer>();
		playerFlipPlayerScript.enabled = false;
		intaractibleObject.SetActive(false);
		textMesh.text = "";
		StartCoroutine(displayText());
		cameraAudioSource.enabled = false;
	}

	void Update()
	{
		if (!pauseMenu.activeSelf && pause)
			Unpause();
		if (Input.GetButtonUp("Pause")) {
			if (pause)
				Unpause();
			else
				Pause();
		}

		if (pause)
			return;

		if (!textFinishedToDisplay)
			return;
		else if (!intaractibleObject.activeSelf) {
			intaractibleObject.SetActive(true);
			waitInteractonWithBed = true;
			return;
		}

		if (waitInteractonWithBed) {
			if (Input.GetButtonUp("Interact") && !tutoPlayerController.canMove && !firefly.activeSelf) {
				firefly.SetActive(true);
				playerFlipPlayerScript.enabled = true;
				cameraAudioSource.enabled = true;
				player.transform.position = respawnPosition;
				tutoPlayerController.canMove = true;
			}
		}
	}
 
	IEnumerator displayText()
	{
		yield return new WaitForSeconds(2f);
		textMesh.text = "Another boring day finally ended...";

		yield return new WaitForSeconds(3.5f);
		textMesh.text = "I think I'll just go to bed...";

		yield return new WaitForSeconds(1.5f);
		textMesh.text = "Like...";

		yield return new WaitForSeconds(3.5f);
		textMesh.text = "Every day of my life...";


		yield return new WaitForSeconds(3.5f);
		textMesh.text = "";

		textFinishedToDisplay = true;
	}

	void Pause()
	{
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
		pause = true;
	}

	void Unpause()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		pause = false;
	}
}