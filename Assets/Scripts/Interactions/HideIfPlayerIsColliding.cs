using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfPlayerIsColliding : MonoBehaviour
{

    [SerializeReference] SpriteRenderer objToHide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            objToHide.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            objToHide.enabled = true;
        }
    }
}
