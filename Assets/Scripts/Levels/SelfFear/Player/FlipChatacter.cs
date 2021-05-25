using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipChatacter : MonoBehaviour
{
    private bool facingRight = true;

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0) {
            GetComponent<Transform>().rotation = Quaternion.Euler(0f, horizontalInput < 0 && !facingRight ? 180f : 0f, 0f);
            facingRight = !facingRight;
        }
    }
}
