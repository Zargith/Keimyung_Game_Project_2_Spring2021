using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyRoundMovement : MonoBehaviour
{
    private Vector3 startPos;

    public float speed = 1;
    public float xScale = 1;
    public float yScale = 1;
    private float delta = 1;

    void Start () {
        startPos = transform.localPosition;
    }

    void Update () {
        Vector3 newPosition = new Vector3(startPos.x, startPos.y, startPos.z);
        newPosition.z += delta * Mathf.Sin(Time.time * speed);
        transform.localPosition = newPosition + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
    }
}
