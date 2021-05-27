using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGameManager : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	public bool pause = false;

	[SerializeField] SpriteRenderer room;
	[SerializeField] Sprite roomFearsFinished;
	[SerializeField] Sprite roomFearsNotFinished;

	[SerializeField] SpriteRenderer mirror;
	[SerializeField] Sprite selfFearFinished;
	[SerializeField] Sprite selfFearNotFinished;

	[SerializeField] SpriteRenderer dresser;
	[SerializeField] Sprite hypochondriacFearFinished;
	[SerializeField] Sprite hypochondriacFearNotFinished;

	[SerializeField] SpriteRenderer gel;
	[SerializeField] Sprite gelHypochondriacFearFinished;
	[SerializeField] Sprite gelHypochondriacFearNotFinished;


	[SerializeField] SpriteRenderer cupboard;
	[SerializeField] Sprite sportFearFinished;
	[SerializeField] Sprite sportFearNotFinished;

	[SerializeField] SpriteRenderer pc;
	[SerializeField] Sprite pcOthersFearFinished;
	[SerializeField] Sprite pcOthersFearNotFinished;
	[SerializeField] SpriteRenderer desk;
	[SerializeField] Sprite deskOthersFearFinished;
	[SerializeField] Sprite deskOthersFearNotFinished;


	void Start()
	{
		Time.timeScale = 1;
		setSpriteIfFearFinished(mirror, "SelfFear", selfFearFinished, selfFearNotFinished);
		setSpriteIfFearFinished(dresser, "HypochondriacFear", hypochondriacFearFinished, hypochondriacFearNotFinished);
		setSpriteIfFearFinished(gel, "HypochondriacFear", gelHypochondriacFearFinished, gelHypochondriacFearNotFinished);
		setSpriteIfFearFinished(cupboard, "SportFear", sportFearFinished, sportFearNotFinished);
		setSpriteIfFearFinished(pc, "OthersFear", pcOthersFearFinished, pcOthersFearNotFinished);
		setSpriteIfFearFinished(desk, "OthersFear", deskOthersFearFinished, deskOthersFearNotFinished);
		string[] fears = new string[] {"SelfFear", "HypochondriacFear", "SportFear", "OthersFear"};
		setSpriteIfAllFearsFinished(room, fears, roomFearsFinished, roomFearsNotFinished);
	}

	void setSpriteIfFearFinished(SpriteRenderer spriteRenderer, string pref, Sprite fearFinished, Sprite fearNotFinished)
	{
		if (PlayerPrefs.GetInt(pref, 0) == 0)
			spriteRenderer.sprite = fearNotFinished;
		else
			spriteRenderer.sprite = fearFinished;
	}

	void setSpriteIfAllFearsFinished(SpriteRenderer spriteRenderer, string[] prefs, Sprite fearsFinished, Sprite fearsNotFinished)
	{
		for (int i = 0; i < prefs.Length; i++)
			if (PlayerPrefs.GetInt(prefs[i], 0) == 0)
				return;
		spriteRenderer.sprite = fearsFinished;
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
