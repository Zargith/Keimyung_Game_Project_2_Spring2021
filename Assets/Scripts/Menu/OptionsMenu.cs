using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
   public void OnResolutionChange() 
    {
        Debug.Log("OptionsMenu - OnResolutionChange");
        //Screen.SetResolution()
    }

    public void OnScreenModeChange()
    {
        Debug.Log("OptionsMenu - OnScreenModeChange");
        //Screen.SetResolution()
    }

    public void OnVolumeChange()
    {
        Debug.Log("OptionsMenu - OnVolumeChange");
        // We'll see when there will be an audiosource
    }
}
