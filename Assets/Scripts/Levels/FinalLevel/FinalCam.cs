using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FinalCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var size = Mathf.Clamp(5 * (transform.position.x / 15f), 5, 8);
        gameObject.GetComponent<Camera>().orthographicSize = size;
        gameObject.GetComponent<FollowPlayer>().SetY(size - 2);
    }
}
