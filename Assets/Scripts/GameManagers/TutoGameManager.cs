using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGameManager : MonoBehaviour
{
	[SerializeField] GameObject firefly;
	[SerializeField] GameObject player;
	[SerializeField] Transform dreamCameraPosition;
	[SerializeField] FollowPlayer cameraFollowPlayerScript;
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
	[SerializeField] float xPositionPlayerStopsRun = 28f;

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
		PlayerPrefs.SetInt("DidTuto", 0);
		PlayerPrefs.SetInt("SelfFear", 0);
		PlayerPrefs.SetInt("HypochondriacFear", 0);
		PlayerPrefs.SetInt("SportFear", 0);
		PlayerPrefs.SetInt("OthersFear", 0);
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

		if (player.transform.position.x >= xPositionPlayerStopsRun && firefly.activeSelf)
			firefly.SetActive(false);

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
				cameraFollowPlayerScript._playerTransform = dreamCameraPosition;
				tutoPlayerController.canMove = true;
			}
		}
	}

	IEnumerator displayText()
	{
		yield return new WaitForSeconds(1.5f);
		textMesh.text = "Another boring day finally ended...";

		yield return new WaitForSeconds(3.5f);
		textMesh.text = "I think I'll just go to bed...";

		yield return new WaitForSeconds(3.5f);
		textMesh.text = "Like...";

		yield return new WaitForSeconds(2f);
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