using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableMenuButton : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject menu;
    [SerializeField] bool pastState;


    void Update()
    {
        if (pastState != menu.activeSelf) {
            pastState = !pastState;
            for (int i = 0 ; i < buttons.Count ; i++)
                buttons[i].enabled = !pastState;
        }
    }

    public void Click()
    {
        menu.SetActive(false);
    }
}
