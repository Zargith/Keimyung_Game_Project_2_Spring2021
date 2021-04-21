using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHeadAnim : MonoBehaviour
{
    public float intensity = 1;
    public float frequency = 1;
    private Vector2 origin;
    private Vector2 n_pos;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        n_pos.x = origin.x;
        n_pos.y = origin.y + Mathf.Sin(Time.time * frequency) * intensity;
        transform.localPosition = n_pos;
    }
}
