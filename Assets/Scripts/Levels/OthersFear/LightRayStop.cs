using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Experimental.Rendering.Universal.Light2D))]
public class LightRayStop : MonoBehaviour
{
    public bool applyChanges = true;

    UnityEngine.Experimental.Rendering.Universal.Light2D fov;
    float initRadius;

    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        initRadius = fov.pointLightOuterRadius;
    }

    float fovRotOffset = 90;
    float hitDist = 0;
    // Update is called once per frame
    void Update()
    {
        var lookDir = new Vector2(
    Mathf.Cos(Mathf.Deg2Rad * (fov.transform.rotation.eulerAngles.z + fovRotOffset)),
    Mathf.Sin(Mathf.Deg2Rad * (fov.transform.rotation.eulerAngles.z + fovRotOffset)));
        Debug.DrawRay(fov.transform.position, lookDir.normalized * initRadius * 0.85f);
        RaycastHit2D hit = Physics2D.Raycast(fov.transform.position, lookDir.normalized, initRadius * 0.85f, LayerMask.GetMask("Ground"));        // If it hits something...
        if (hit.collider != null)
        {
            hitDist = hit.distance;
        } else
        {
            hitDist = initRadius;
        }
        if (applyChanges)
            fov.pointLightOuterRadius = hitDist;
    }

    public float GetMaxDist()
    {
        return hitDist;
    }
}
