using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{

	[SerializeField] AudioMixer mixer;
	[SerializeField] string exposedGroupeName;
	[SerializeField] Slider slider;


	void Start()
	{
		slider.value = PlayerPrefs.GetFloat(exposedGroupeName, slider.maxValue);
	}

	public void SetVolume()
	{
		float sliderValue = slider.value;
		Debug.Log(Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20);
		mixer.SetFloat(exposedGroupeName, Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20);
		PlayerPrefs.SetFloat(exposedGroupeName, sliderValue);
	}
}
