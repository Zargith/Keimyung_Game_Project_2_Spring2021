using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float speed;
    float savedSpeed;
    SpriteRenderer a;

    private void OnEnable()
    {
        SportGameOver.OnGameOver += stop;
        SportGameOver.OnRestart += restart;
        a = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        SportGameOver.OnGameOver -= stop;
        SportGameOver.OnRestart -= restart;

    }


    void Update()
    {
        a.material.mainTextureOffset = new Vector2(Time.time * speed, 0f);
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
