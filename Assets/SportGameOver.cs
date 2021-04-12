using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportGameOver : MonoBehaviour
{
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    [SerializeField] float _deathHeight;


    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y < _deathHeight)
        {
            OnGameOver?.Invoke();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(10, _deathHeight, 0), new Vector3(-10, _deathHeight, 0));
    }

}
