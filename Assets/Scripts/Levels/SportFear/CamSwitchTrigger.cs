using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour
{

    public CameraFollowPLayerSmooth cam;

    public bool changeTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam._followPlayer = changeTo;
        }
    }

}
