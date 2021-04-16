using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIfPlayerIsClose : MonoBehaviour
{
    public GameObject player;
    Vector2 playerPosition;
    public GameObject elemToDisplay;
    public float distance;

    void Update()
    {
        playerPosition = player.transform.position;

        Debug.Log(Vector2.Distance(playerPosition, this.transform.position));
        if (Vector2.Distance(playerPosition, this.transform.position) <= distance)
            elemToDisplay.SetActive(true);
        else
            elemToDisplay.SetActive(false);
    }
}
