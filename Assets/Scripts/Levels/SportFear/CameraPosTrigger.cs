using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosTrigger : MonoBehaviour
{

    CameraFollowPLayerSmooth cfps;
    [SerializeField] Vector3 pos;

    void Start()
    {
        cfps = FindObjectOfType<CameraFollowPLayerSmooth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cfps.setPosition(pos);
        }
    }

}
