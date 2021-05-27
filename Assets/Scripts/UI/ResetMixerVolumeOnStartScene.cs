using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ResetMixerVolumeOnStartScene : MonoBehaviour
{

	[SerializeField] AudioMixer mixer;
	[SerializeField] string exposedGroupeNameMaster;
	[SerializeField] string exposedGroupeNameMusic;
	[SerializeField] string exposedGroupeNameSoundEffects;

	void Start()
	{
		float masterValume = PlayerPrefs.GetFloat(exposedGroupeNameMaster, 0.0001f);
		mixer.SetFloat(exposedGroupeNameMaster, Mathf.Log10(Mathf.Max(masterValume, 0.0001f)) * 20);
		float musicValume = PlayerPrefs.GetFloat(exposedGroupeNameMusic, 0.0001f);
		mixer.SetFloat(exposedGroupeNameMusic, Mathf.Log10(Mathf.Max(musicValume, 0.0001f)) * 20);
		float soundEffectsValume = PlayerPrefs.GetFloat(exposedGroupeNameSoundEffects, 0.0001f);
		mixer.SetFloat(exposedGroupeNameSoundEffects, Mathf.Log10(Mathf.Max(soundEffectsValume, 0.0001f)) * 20);
	}
}
