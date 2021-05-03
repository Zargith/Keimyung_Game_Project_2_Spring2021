using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPLayerSmooth : MonoBehaviour
{

    Camera cam;
    Transform player;
    float playerOffset;
    float prevOffset;

    Vector3 origin;

    //CAM UP 
    float elapsedTimeU;
    bool haveRisen= false;

    //lerp var
    float elapsedTimeHaveToMoveU;
    bool haveToMoveU = false;


    //CAM DOWN 
    float elapsedTimeB;
    bool haveFallen = false;

    //lerp var
    float elapsedTimeHaveToMoveB;
    bool haveToMoveB = false;

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
        adjustDownCam(viewPos);
        adjustDUpCam(viewPos);

        prevOffset = transform.position.y - player.position.y;
    }

    void adjustDUpCam(Vector3 viewPos)
    {
        if (viewPos.y > 0.75)
        {
            transform.position = new Vector3(0, player.position.y + prevOffset, -10);
            elapsedTimeB = 0;
            elapsedTimeHaveToMoveU = 0;
            haveRisen = true;
            haveToMoveU = false;
        }
        else if (viewPos.y < 0.75 && haveRisen)
        {
            if (elapsedTimeHaveToMoveU > 1)
            {
                haveRisen = false;
            }
            elapsedTimeU += Time.deltaTime;
            if (elapsedTimeU > 1 && !haveToMoveU)
            {
                origin = transform.position;
                haveToMoveU = true;
                elapsedTimeHaveToMoveU = 0;
            }
            if (haveToMoveU)
            {
                transform.position = Vector3.Lerp(origin, new Vector3(0, player.position.y + playerOffset, -10), elapsedTimeHaveToMoveU);
                elapsedTimeHaveToMoveU += Time.deltaTime;
            }
        }
    }

    void adjustDownCam(Vector3 viewPos)
    {
        if (viewPos.y < 0)
        {
            transform.position = new Vector3(0, player.position.y + prevOffset, -10);
            elapsedTimeB = 0;
            elapsedTimeHaveToMoveB = 0;
            haveFallen = true;
            haveToMoveB = false;
        }
        else if (viewPos.y > 0 && haveFallen)
        {
            if (elapsedTimeHaveToMoveB > 1)
            {
                haveFallen = false;
            }
            elapsedTimeB += Time.deltaTime;
            if (elapsedTimeB > 1 && !haveToMoveB)
            {
                origin = transform.position;
                haveToMoveB = true;
                elapsedTimeHaveToMoveB = 0;
            }
            if (haveToMoveB)
            {
                transform.position = Vector3.Lerp(origin, new Vector3(0, player.position.y + playerOffset, -10), elapsedTimeHaveToMoveB);
                elapsedTimeHaveToMoveB += Time.deltaTime;
            }
        }
    }

    public void replace()
    {
        transform.position = new Vector3(0, 0, -10);
    }

}
