using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirorLevelMoveClone : MonoBehaviour
{
    private Animator _anim;
    private Vector3 advance = Vector3.zero;
    [SerializeField] private GameObject clone;
    private bool hasJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = clone.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        advance = GetComponent<MirorLevelMove>().GetAdvance();
        bool jump = GetComponent<MirorLevelMove>().GetJump();
        _anim.SetBool("isGrounded", GetComponent<MirorLevelMove>().GetJumpAnim());
        _anim.SetFloat("yVelocity", GetComponent<Rigidbody2D>().velocity.y);

        if (jump && !hasJumped) {
            hasJumped = true;
            clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, GetComponent<MirorLevelMove>().GetJumpHeight()), ForceMode2D.Impulse);
        } else {
            if (!jump)
                hasJumped = false;
            clone.GetComponent<Transform>().position += advance * -1;

            _anim.SetBool("isRunning", advance.x != 0);
        }
    }
}
