using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBtn : MonoBehaviour
{
    [SerializeField] private GameObject btn1;
    [SerializeField] private GameObject btn2;
    [SerializeField] private GameObject jmp1;
    [SerializeField] private GameObject jmp2;

    private float time = 0f;


    private void Update()
    {
        time += Time.deltaTime;
        // le bouton peut être activé et on saute dessus
        if (btn1.activeSelf && btn1.GetComponent<ButtonColision>().GetCollideState()) {
            btn1.SetActive(false); // on cache le bouton
            btn2.SetActive(true); // pour montré qu'il est activé
            time = 0f;
        } else if (time >= 0.5f && btn2.activeSelf && !btn2.GetComponent<ButtonColision>().GetCollideState()) {
            btn1.SetActive(true);
            btn2.SetActive(false);
        }
    }
}
