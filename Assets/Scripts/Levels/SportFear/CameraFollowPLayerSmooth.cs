using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPLayerSmooth : MonoBehaviour
{

    Camera cam;
    Transform player;
    float playerOffset;
    float prevOffset;
    float elapsedTime;
    bool haveFallen = false;

    //lerp var
    float elapsedTimeHaveToMove;
    bool haveToMove = false;
    Vector3 origin;

    private void OnEnable()
    {
        SportGameOver.OnRestart += replace;
    }

    private void OnDisable()
    {
        SportGameOver.OnRestart -= replace;
    }


    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerOffset = transform.position.y - player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(player.position);
        if (viewPos.y < 0)
        {
            transform.position = new Vector3(0, player.position.y + prevOffset, -10);
            elapsedTime = 0;
            haveFallen = true;
            haveToMove = false;
        } else if (viewPos.y > 0 && haveFallen)
        {
            if( elapsedTimeHaveToMove > 1)
            {
                haveFallen = false;
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 1 && !haveToMove)
            {
                origin = transform.position;
                haveToMove = true;
                elapsedTimeHaveToMove = 0;
            }
            if (haveToMove)
            {
                transform.position = Vector3.Lerp(origin, new Vector3(0, player.position.y + playerOffset, -10), elapsedTimeHaveToMove);
                elapsedTimeHaveToMove += Time.deltaTime;
            }
        }
        prevOffset = transform.position.y - player.position.y;
    }

    public void replace()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
