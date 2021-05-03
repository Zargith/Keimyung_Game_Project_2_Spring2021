using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    Transform player;
    MapScroller map;
    Animator anim;
    CameraFollowPLayerSmooth cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        map = GameObject.FindObjectOfType<MapScroller>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindObjectOfType<CameraFollowPLayerSmooth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            player.GetComponent<PlayerSportController>().stop();
            map.stop();
            anim.SetBool("collected", true);
            Invoke("selfDestroy", 3);
        }
    }

    void selfDestroy()
    {
        player.GetComponent<PlayerSportController>().revive();
        map.restart();
        cam.replace();
        map.removeFromChunk(transform);
    }
   
}
