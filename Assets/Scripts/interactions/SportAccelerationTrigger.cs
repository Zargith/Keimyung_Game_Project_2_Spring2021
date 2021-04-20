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

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= player.position.x)
        {
            map.addSpeed(_speedAdded);
            map.removeFromChunk(transform);
        }
    }
}
