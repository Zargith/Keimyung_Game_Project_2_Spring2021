using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur2 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool _hasBeenActivated;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyUp(KeyCode.E)) {
            _hasBeenActivated = true;
        }
    }

    private void Update()
    {
        if (door.activeSelf && _hasBeenActivated)
            door.SetActive(false);
    }

    public bool GetActiveState()
    {
        return _hasBeenActivated;
    }
}
