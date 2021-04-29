using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportHigherJumpTrigger : MonoBehaviour
{
    [SerializeField] float _forceAdded = 0.5f;
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
            player.GetComponent<PlayerSportController>().addForce(_forceAdded);
            map.removeFromChunk(transform);
        }
    }
}
