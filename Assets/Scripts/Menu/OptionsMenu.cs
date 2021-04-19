using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public string keyBindScene;
   public void OnResolutionChange() 
    {
        //Screen.SetResolution()
    }

    public void OnScreenModeChange()
    {

    }

    public void OnVolumeChange()
    {

    }

    public void KeyBind()
    {
        SceneManager.LoadScene(keyBindScene);
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // TODO
        // Le back est a revoir aussi vu que c'est sur le build index et on veut un custom index -> si on click sur options dans pause et qu'on fait back on veut pas retourner sur le start menu
    }
}
