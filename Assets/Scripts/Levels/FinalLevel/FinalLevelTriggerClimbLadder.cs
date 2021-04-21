using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelTriggerClimbLadder : MonoBehaviour
{
    public FinalPlayer finalPlayer;
    public GameObject indicator;
    public Collider2D end;
    public Collider2D top;
    Recoloring[] components;
    bool hasReachTop = false;

    private void Start()
    {
        components = GameObject.FindObjectsOfType<Recoloring>();
    }

    // Update is called once per frame
    void Update()
    {
        if (indicator.activeSelf && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Interact")))
        {
            end.enabled = false;
            top.isTrigger = true;
            finalPlayer.Climb(true);
            foreach (var item in components)
            {
                item.StartColoring();
            }
        }
        if (!hasReachTop && top.IsTouching(finalPlayer.gameObject.GetComponent<Collider2D>()))
        {
            hasReachTop = true;
        }
        if (hasReachTop && !top.IsTouching(finalPlayer.gameObject.GetComponent<Collider2D>()))
        {
            top.isTrigger = false;
            finalPlayer.Climb(false);
            end.enabled = true;

        }
    }


}
