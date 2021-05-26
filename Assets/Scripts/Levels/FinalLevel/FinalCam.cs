using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FinalCam : MonoBehaviour
{

    float initSize;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        initSize = gameObject.GetComponent<Camera>().orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    float offsetY;
    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<FollowPlayer>()._playerTransform != null)
        {
            var size = Mathf.Clamp(5.0f * (transform.position.x / 15f), initSize, 8.0f);
            gameObject.GetComponent<Camera>().orthographicSize = size;
            gameObject.GetComponent<FollowPlayer>().SetY(size - offsetY);
        }
        else
        {
            offsetY = initSize - (transform.position.y - player.transform.position.y);
        }
    }
}
