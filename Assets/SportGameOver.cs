using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportGameOver : MonoBehaviour
{
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    [SerializeField] Vector2 _deathMin;


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
            OnGameOver?.Invoke();
        }
        if (player.position.y < _deathMin.y)
        {
            OnGameOver?.Invoke();
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_deathMin.x, -5, 0), new Vector3(_deathMin.x, 5, 0));
        Gizmos.DrawLine(new Vector3(10, _deathMin.y, 0), new Vector3(-10, _deathMin.y, 0));
    }

}
