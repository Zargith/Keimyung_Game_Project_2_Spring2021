using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSportDeath : MonoBehaviour
{

    SportGameOver sgo;

    private void Awake()
    {
        sgo = GameObject.FindObjectOfType<SportGameOver>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sgo.death();
        }
    }

}
