using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float speed;
    float savedSpeed;
    [SerializeField] float dest;
    [SerializeField] float rest;


    private void OnEnable()
    {
        SportGameOver.OnGameOver += stop;
        SportGameOver.OnRestart += restart;
    }

    private void OnDisable()
    {
        SportGameOver.OnGameOver -= stop;
        SportGameOver.OnRestart -= restart;

    }


    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= dest)
        {
            transform.position = new Vector3(rest, transform.position.y, transform.position.z);
        }
    }

    public void stop()
    {
        savedSpeed = speed == 0 ? savedSpeed : speed;
        speed = 0;
    }

    public void restart()
    {
        speed = savedSpeed;
    }



}
