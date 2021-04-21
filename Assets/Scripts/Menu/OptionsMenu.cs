using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropDown;

    public TMPro.TMP_Dropdown screenModeDropDown;

    private int resolutionX = 1280;

    private int resolutionY = 720;

    private bool fullscreen = false;


    public Slider musicVolumeSlider;

    public AudioMixer mixer;
    public void OnResolutionChange() 
    {
        string value = resolutionDropDown.options[resolutionDropDown.value].text;
        Debug.Log("OptionsMenu - OnResolutionChange: " + value);

        string[] resolutions = value.Split('x');

        resolutionX = int.Parse(resolutions[0]);
        resolutionY = int.Parse(resolutions[1]);
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);
    }

    public void OnScreenModeChange()
    {
        string value = screenModeDropDown.options[screenModeDropDown.value].text;
        Debug.Log("OptionsMenu - OnScreenModeChange: " + value);

        if (value == "FullScreen")
            fullscreen = true;
        else
            fullscreen = false;
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);
    }

    public void OnVolumeChange()
    {
        float value = musicVolumeSlider.value;
        Debug.Log("OptionsMenu - OnVolumeChange: " + value);
        mixer.SetFloat("MusicVolume", value);
    }
}
