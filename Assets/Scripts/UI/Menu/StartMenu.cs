using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	[SerializeField] string TutoSceneName;
	[SerializeField] string RoomSceneName;

	public void NewGame()
	{
		SceneManager.LoadScene(TutoSceneName);
	}

	public void ContinueGame()
	{
		SceneManager.LoadScene(RoomSceneName);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
