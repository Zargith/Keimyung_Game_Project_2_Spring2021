using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoor : MonoBehaviour
{

    [SerializeField] GameObject chain1;
    [SerializeField] GameObject chain2;
    [SerializeField] GameObject chain3;
    [SerializeField] GameObject chain4;

    int finished = 0;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SportFear", 0) == 1)
        {
            finished++;
            chain1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("OthersFear", 0) == 1)
        {
            finished++;
            chain2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SelfFear", 0) == 1)
        {
            finished++;
            chain3.SetActive(false);
        }
        if (PlayerPrefs.GetInt("HypochondriacFear", 0) == 1)
        {
            finished++;
            chain4.SetActive(false);
        }

        if (finished >= 4)
        {
            GetComponent<DisplayIfPlayerIsClose>().enabled = true;
        }
    }

}
