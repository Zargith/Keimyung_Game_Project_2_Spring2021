using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersFearGameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool pause = false;

    // Start is called before the first frame update
    void Start() {/*only here to have an enablable script*/}

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.activeSelf && pause)
            Unpause();
        if (Input.GetButtonUp("Pause"))
        {
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
