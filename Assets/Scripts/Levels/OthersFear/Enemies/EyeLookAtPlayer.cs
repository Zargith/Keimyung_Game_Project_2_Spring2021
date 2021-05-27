using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookAtPlayer : MonoBehaviour
{
    public Vector2 mvtScale;
    [SerializeReference] UnityEngine.Experimental.Rendering.Universal.Light2D fov;

    Transform player;
    Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anchor = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var dist = (player.transform.position + new Vector3(0, 1, 0) - anchor.position);
        var dir = dist.normalized;

        fov.transform.rotation = Quaternion.Euler(0, 0, Mathf.Acos(dir.x) * (dir.y > 0 ? 1 : -1) * Mathf.Rad2Deg - 90);
        dir.x = dir.x * mvtScale.x;
        dir.y = dir.y * mvtScale.y;
        transform.localPosition = dir * Mathf.Min(dist.magnitude / 10.0f, 1);
    }
}
