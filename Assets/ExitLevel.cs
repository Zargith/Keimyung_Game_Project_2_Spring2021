using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    enum EnumLevels
    {
        MYSOPHOBIA,
        OTHERS_FEAR,
        SELF_FEAR,
        SPORT_FEAR
    }

    [SerializeField] GameObject trigger;
    [SerializeField] EnumLevels level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.activeSelf && Input.GetButtonDown("Interact"))
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt(LevelPref, 1);
            SceneManager.LoadScene("Room");
        }
    }

    string LevelPref
    {
        get
        {
            return level switch
            {
                EnumLevels.MYSOPHOBIA => "HypochondriacFear",
                EnumLevels.OTHERS_FEAR => "OthersFear",
                EnumLevels.SELF_FEAR => "SelfFear",
                EnumLevels.SPORT_FEAR => "SportFear",
                _ => null,
            };
        }
    }
}
