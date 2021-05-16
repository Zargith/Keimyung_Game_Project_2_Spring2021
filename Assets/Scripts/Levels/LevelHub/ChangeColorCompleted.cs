using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeColorCompleted : MonoBehaviour
{

    [SerializeField] string playerPrefName;
    [SerializeField] Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(playerPrefName, 0) == 1)
        {
            light.color = new Color(0, 1, 0);
        }
    }

}
