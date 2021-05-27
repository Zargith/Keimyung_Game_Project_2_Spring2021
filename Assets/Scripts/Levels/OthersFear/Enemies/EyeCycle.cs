using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCycle : MonoBehaviour
{

    [SerializeField] float halfCycle;
    [SerializeField] float changeDuration;
    [SerializeReference] UnityEngine.Experimental.Rendering.Universal.Light2D fov;
    [SerializeReference] LightRayStop lightRay;

    float time = 0;
    float initRadius;
    Vector3 initScale;
    bool closed = false;

    // Start is called before the first frame update
    void Start()
    {
        initRadius = fov.pointLightOuterRadius;
        initScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (halfCycle == 0)
            return;
        time += Time.deltaTime;
        if (time > halfCycle)
        {
            closed = !closed;
            time = 0;
            if (closed)
            {
                fov.pointLightOuterRadius = 0;
                transform.localScale = new Vector3(initScale.x, 0, initScale.z);
                lightRay.applyChanges = false;
            }
            else
            {
                fov.pointLightOuterRadius = initRadius;
                transform.localScale = initScale;
                lightRay.applyChanges = true;
            }
        }
        else if (time > halfCycle - changeDuration)
        {
            lightRay.applyChanges = false;
            var tm = halfCycle - time;
            if (closed)
            {
                fov.pointLightOuterRadius = Mathf.Min(initRadius * ((changeDuration - tm) / changeDuration), lightRay.GetMaxDist());
                var scale = initScale;
                scale.y = scale.y * ((changeDuration - tm) / changeDuration);
                transform.localScale = scale;
            }
            else
            {
                fov.pointLightOuterRadius =  Mathf.Min(initRadius * (tm / changeDuration), lightRay.GetMaxDist());
                var scale = initScale;
                scale.y = scale.y * (tm / changeDuration);
                transform.localScale = scale;
            }
        }
    }
}
