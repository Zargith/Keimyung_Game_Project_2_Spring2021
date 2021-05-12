using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityFlipPlayer : MonoBehaviour
{
    bool facingRight = true;

    void Update()
    {
        var horizontalInput = GetComponent<Rigidbody2D>().velocity.x;
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public bool isPlayerFacingRight()
    {
        return facingRight;
    }
}
