using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur2 : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (Input.GetKeyUp(KeyCode.E)) {
            door.SetActive(false);
        }
    }
}
