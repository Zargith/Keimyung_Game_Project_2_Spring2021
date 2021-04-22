using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColision : MonoBehaviour
{
    private bool collide = false;
    private bool collideNmy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collide = true;
        } else if (collision.gameObject.tag == "Nmy") {
            collideNmy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collide = false;
        } else if( collision.gameObject.tag == "Nmy") {
            collideNmy = true;
        }
    }

    public bool GetCollideState()
    {
        return collide;
    }

    public bool GetCollideNmyState()
    {
        return collideNmy;
    }
}
