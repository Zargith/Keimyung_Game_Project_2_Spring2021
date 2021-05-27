using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SportEndDoor : MonoBehaviour
{

    [SerializeField] GameObject obj;
    MapScroller map;
    Paralax[] pp;
    bool touched = false;

    private void Start()
    {
        map = FindObjectOfType<MapScroller>();
        pp = FindObjectsOfType<Paralax>();

    }

    private void Update()
    {
        if (touched && Input.GetButtonDown("Interact"))
        {
            PlayerPrefs.SetInt("SportFear", 1);
            SceneManager.LoadScene("Room");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            map.stop();
            collision.GetComponent<PlayerSportController>().stop();
            GameObject.Find("BALL").SetActive(false);
            obj.SetActive(true);
            touched = true;
            foreach (Paralax p in pp)
            {
                p.stop();
            }
        }
    }


}
