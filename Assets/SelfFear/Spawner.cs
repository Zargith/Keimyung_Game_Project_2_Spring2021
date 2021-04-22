using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject perso;
    [SerializeField] private GameObject clone;
    [SerializeField] private GameObject[] interrupteur;
    Vector3 posPerso;
    Vector3 posClone;

    void Awake()
    {
        posClone = clone.transform.position;
        posPerso = perso.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (perso.GetComponent<ButtonColision>().GetCollideNmyState() || clone.GetComponent<ButtonColision>().GetCollideNmyState()) {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        perso.transform.position = posPerso;
        clone.transform.position = posClone;
        for (int i = 0 ; i < interrupteur.Length ; ++i) {
            interrupteur[i].GetComponent<Reset>().ResetInterrupteur();
        }
    }
}
