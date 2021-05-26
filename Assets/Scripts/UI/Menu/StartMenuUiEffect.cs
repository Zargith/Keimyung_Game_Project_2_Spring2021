using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class StartMenuUiEffect : MonoBehaviour
{
    [SerializeField] float switchTime = 1f;
    [SerializeField] float stillTime = 30f;
    [SerializeField] GameObject light_menu;
    [SerializeField] GameObject dark_menu;
    [SerializeField] GameObject light_char;
    [SerializeField] GameObject dark_char;

    float time = 8f;
    bool isDarkActive = false;
    bool changeDone = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= stillTime && changeDone) {
            StartCoroutine(isDarkActive ? "ToLight" : "ToDark");
            isDarkActive = !isDarkActive;
            changeDone = false;
        }
    }

    IEnumerator ToLight()
    {
        float timeToWait = switchTime / 6f;

        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait / 5);
        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait / 5);
        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait * 2);
        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait / 10);
        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait / 10);
        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait / 10);
        ChangeActiveLayer(true, false);
        light_menu.SetActive(true);
        dark_menu.SetActive(false);
        changeDone = true;
        time = 0;
    }

    IEnumerator ToDark()
    {
        float timeToWait = switchTime / 6f;

        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait/5);
        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait / 5);
        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait * 2);
        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait / 7);
        ChangeActiveLayer(false, true);
        yield return new WaitForSeconds(timeToWait / 7);
        ChangeActiveLayer(true, false);
        yield return new WaitForSeconds(timeToWait / 7);
        ChangeActiveLayer(false, true);
        light_menu.SetActive(false);
        dark_menu.SetActive(true);
        changeDone = true;
        time = 0;
    }

    void ChangeActiveLayer(bool light, bool dark)
    {
        light_char.SetActive(light);
        dark_char.SetActive(dark);

    }

}
