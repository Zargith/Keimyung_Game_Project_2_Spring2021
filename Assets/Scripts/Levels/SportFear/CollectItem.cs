using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    Transform player;
    MapScroller map;
    Animator anim;
    CameraFollowPLayerSmooth cam;
    public GameObject ToShow;
    Paralax[] pp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        map = GameObject.FindObjectOfType<MapScroller>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindObjectOfType<CameraFollowPLayerSmooth>();
        pp = FindObjectsOfType<Paralax>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            player.GetComponent<PlayerSportController>().stop();
            map.stop();
            foreach(Paralax p in pp)
            {
                p.stop();
            }
            anim.SetBool("collected", true);
            Invoke("selfDestroy", 3);
            ToShow.SetActive(true);
        }
    }

    void selfDestroy()
    {
        player.GetComponent<PlayerSportController>().revive();
        map.restart();
        cam.replace();
        map.removeFromChunk(transform);
        foreach (Paralax p in pp)
        {
            p.restart();
        }
    }
   
}
