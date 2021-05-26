using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropDown;
    public TMPro.TMP_Dropdown screenModeDropDown;

    int resolutionX = 1280;
    int resolutionY = 720;
    bool fullscreen = false;

    public void OnResolutionChange() 
    {
        string value = resolutionDropDown.options[resolutionDropDown.value].text;
        string[] resolutions = value.Split('x');

        resolutionX = int.Parse(resolutions[0]);
        resolutionY = int.Parse(resolutions[1]);
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);
    }

    public void OnScreenModeChange()
    {
        string value = screenModeDropDown.options[screenModeDropDown.value].text;

        if (value == "FullScreen")
            fullscreen = true;
        else
            fullscreen = false;
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);
    }
}
