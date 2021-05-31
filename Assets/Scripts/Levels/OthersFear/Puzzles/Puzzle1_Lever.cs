using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Lever : MonoBehaviour
{
    [SerializeReference] GameObject target;
    [SerializeField] Vector2 flip;
    [SerializeReference] GameObject trigger;
    [SerializeReference] GameObject animate;

    int start = 0;
    float time = 0;
    float y_initPos;
    float pullTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        y_initPos = animate.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (start == 0 && trigger.activeSelf && Input.GetButton("Interact"))
        {
            start = 1;
            time = 0;
        }
        if (start == 1)
        {
            animate.transform.localPosition = new Vector3(0, y_initPos + (-time * time + time * pullTime * 2) * pullTime * 8 * (y_initPos * -2), 0);
            if (time > pullTime)
            {
                start = 2;
                Puzzles_Func.FlipTarget(target, flip);
            }
        }
        else if (start == 2)
        {
            animate.transform.localPosition = new Vector3(0, y_initPos + (-time * time + time * pullTime * 2) * pullTime * 8 * (y_initPos * -2), 0);
            if (time > pullTime * 2)
            {
                start = 0;
                animate.transform.localPosition = new Vector3(0, y_initPos, 0);
            }
        }
    }
}
