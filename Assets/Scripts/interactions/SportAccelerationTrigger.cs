using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportAccelerationTrigger : MonoBehaviour
{
    [SerializeField] float _speedAdded = 0.5f;
    Transform player;
    MapScroller map;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        map = GameObject.FindObjectOfType<MapScroller>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            map.addSpeed(_speedAdded);
            map.removeFromChunk(transform);
        }
    }
}
