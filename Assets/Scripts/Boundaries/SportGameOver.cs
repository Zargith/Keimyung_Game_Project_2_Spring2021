using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportGameOver : MonoBehaviour
{
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public delegate void Restart();
    public static event Restart OnRestart;

    [SerializeField] Vector2 _deathMin;

    private bool pepsi = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x < _deathMin.x)
        {
            death();
        }

    }

    void restart()
    {
        OnRestart?.Invoke();
        pepsi = false;
    }

    public void death()
    {
        if (pepsi) return;
        pepsi = true;
        OnGameOver?.Invoke();
        Invoke("restart", 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_deathMin.x, -5, 0), new Vector3(_deathMin.x, 5, 0));
    }

}
