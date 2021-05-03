using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoloring : MonoBehaviour
{
    SpriteRenderer spr;
    Color baseColor;
    Color target;
    [SerializeField] float duration = 5.0f;
    private bool recolor;
    private float start;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        baseColor = spr.color;

        target = Random.ColorHSV(0, 1, 0.3f, 0.6f, 1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (recolor)
        {
            float dt = Time.time - start;
            if (dt <= duration)
            {
                spr.color = Color.Lerp(baseColor, target, dt / duration);
            }
            else
            {
                spr.color = target;
            }
        }
    }

    public void StartColoring()
    {
        recolor = true;
        start = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && recolor == false)
        {
            StartColoring();
        }
    }
}
