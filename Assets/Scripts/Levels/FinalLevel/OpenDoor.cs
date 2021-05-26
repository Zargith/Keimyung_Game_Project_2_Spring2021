using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeReference] FollowPlayer playerCam;
    [SerializeReference] FinalPlayer player;
    [SerializeReference] FireflyDecreaseMaxIntensityThroughLevel2D firefly;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Interact"))
        {
            foreach (var item in GetComponentsInParent<BoxCollider2D>())
            {
                item.isTrigger = true;
            }
            playerCam.SetTarget(player.transform);
            player.playerSpeed = playerSpeed;
        }
    }
}
