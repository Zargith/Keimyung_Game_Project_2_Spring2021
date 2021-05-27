using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollowPLayerSmooth : MonoBehaviour
{
    float MIN = 0;

    Camera cam;
    Transform player;
    float playerOffset;
    float prevOffset;

    Vector3 origin;

    //CAM DOWN 
    float elapsedTimeB;
    bool haveFallen = false;

    //lerp var
    float elapsedTimeHaveToMoveB;
    bool haveToMoveB = false;


    //ok
    bool isPositioning = false;
    Vector3 va;
    Vector3 vb;
    float vt;

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

        if (isPositioning)
        {
            transform.position = Vector3.Lerp(va, vb, vt / 6);
            if (vt/6 > 1) isPositioning = false;
            vt += Time.deltaTime;
        } else
        {
            Vector3 viewPos = cam.WorldToViewportPoint(player.position);
            adjustDownCam(viewPos);
            prevOffset = transform.position.y - player.position.y;
        }

    }

    void adjustDownCam(Vector3 viewPos)
    {
        if (viewPos.y < MIN)
        {
            transform.position = new Vector3(0, player.position.y + prevOffset, -10);
            elapsedTimeB = 0;
            elapsedTimeHaveToMoveB = 0;
            haveFallen = true;
            haveToMoveB = false;
        }
        else if (viewPos.y > MIN && haveFallen)
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


    public void setPosition(Vector3 b)
    {
        va = transform.position;
        vb = b;
        isPositioning = true;
        vt = 0;
    }

    public void replace()
    {
        haveFallen = false;
        haveToMoveB = false;
        isPositioning = false;
        elapsedTimeB = 0;
        elapsedTimeHaveToMoveB = 0;
        transform.position = new Vector3(0, 0, -10);
    }

}
