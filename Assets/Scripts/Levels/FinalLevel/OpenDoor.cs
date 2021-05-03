using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] FollowPlayer playerCam;
    [SerializeField] FinalPlayer player;
    [SerializeField] FireflyDecreaseMaxIntensityThroughLevel2D firefly;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Interact"))
        {
            foreach (var item in GetComponentsInParent<BoxCollider2D>())
            {
                item.enabled = false;
            }
            playerCam.SetTarget(player.transform);
        }
    }
}
