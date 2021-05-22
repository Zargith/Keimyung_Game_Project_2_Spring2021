using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    [SerializeField] private GameObject[] door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetInterrupteur()
    {
        for (int i = 0 ; i < door.Length ; ++i)
            door[i].SetActive(true);
    }
}
