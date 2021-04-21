using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoActivateFireflyFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject firefly;
    [SerializeField] GameObject player;
    TutoPlayerController tutoPlayerController;
    FlipPlayer playerFlipPlayerScript;
    [SerializeField] GameObject intaractibleObject;
    [SerializeField] TextMesh textMesh;
    bool textFinishedToDisplay = false;
    [SerializeField] AudioSource cameraAudioSource;
    bool waitInteractonWithBed = false;
    [SerializeField] Vector2 respawnPosition;


    void Start()
    {
        firefly.SetActive(false);
        tutoPlayerController = player.GetComponent<TutoPlayerController>();
        playerFlipPlayerScript = tutoPlayerController.GetComponent<FlipPlayer>();
        playerFlipPlayerScript.enabled = false;
        intaractibleObject.SetActive(false);
        textMesh.text = "";
        StartCoroutine(displayText());
        cameraAudioSource.enabled = false;
    }

    void Update()
    {
        if (!textFinishedToDisplay)
            return;
        else if (!intaractibleObject.activeSelf) {
            intaractibleObject.SetActive(true);
            waitInteractonWithBed = true;
            return;
        }

        if (waitInteractonWithBed) {
            if (Input.GetButtonDown("Interact") && !tutoPlayerController.canMove && !firefly.activeSelf) {
                firefly.SetActive(true);
                playerFlipPlayerScript.enabled = true;
                cameraAudioSource.enabled = true;
                player.transform.position = respawnPosition;
                tutoPlayerController.canMove = true;
            }
        }
    }
 
    IEnumerator displayText()
    {
        yield return new WaitForSeconds(1.5f);
        textMesh.text = "Again another boring day...";

        yield return new WaitForSeconds(3.5f);
        textMesh.text = "I think I'll just go to bed...";

        yield return new WaitForSeconds(3.5f);
        textMesh.text = "Like every annoying day of my life...";

        yield return new WaitForSeconds(3.5f);
        textMesh.text = "";

        textFinishedToDisplay = true;
    }
}
