using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Lever : MonoBehaviour
{
    enum Axis
    {
        X,
        X_,
        Y,
        Y_,
        Z,
        Z_
    }

    [SerializeReference] GameObject target;
    [SerializeField] Axis axis;
    [SerializeField] float amount = 180;
    [SerializeReference] GameObject trigger;
    [SerializeReference] GameObject animate;

    int start = 0;
    float time = 0;
    float y_initPos;
    float pullTime = 0.5f;
    Vector3 rotation;

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
                rotation = target.transform.rotation.eulerAngles;
            }
        }
        else if (start == 2)
        {
            float progress = (time - pullTime) / pullTime;
            FlipTarget(Mathf.Clamp01(progress));
            animate.transform.localPosition = new Vector3(0, y_initPos + (-time * time + time * pullTime * 2) * pullTime * 8 * (y_initPos * -2), 0);
            if (time > pullTime * 2)
            {
                start = 0;
                animate.transform.localPosition = new Vector3(0, y_initPos, 0);
                FlipTarget(1);
            }
        }
    }

    void FlipTarget(float progress)
    {
        Vector3 tmp = rotation;
        switch (axis)
        {
            case Axis.X:
                tmp.x += amount * progress;
                break;
            case Axis.X_:
                tmp.x -= amount * progress;
                break;
            case Axis.Y:
                tmp.y += amount * progress;
                break;
            case Axis.Y_:
                tmp.y -= amount * progress;
                break;
            case Axis.Z:
                tmp.z += amount * progress;
                break;
            case Axis.Z_:
                tmp.z -= amount * progress;
                break;
        }
        target.transform.rotation = Quaternion.Euler(tmp);
    }
}
