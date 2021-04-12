using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _drawLimits;
    [SerializeField] List<Transform> _chunks = new List<Transform>();

    private void OnEnable()
    {
        SportGameOver.OnGameOver += stopScroll;
    }

    private void OnDisable()
    {
        SportGameOver.OnGameOver -= stopScroll;
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            _chunks.Add(child);
        }
    }
    
    void Update()
    {
        List<Transform> toDestroy = new List<Transform>();
        foreach (Transform ch in _chunks)
        {
                ch.position += Vector3.left * _speed * Time.deltaTime;
                if (ch.position.x <= _drawLimits.y)
                {
                    ch.gameObject.SetActive(true);
                }
                else
                {
                    ch.gameObject.SetActive(false);
                }
                if (ch.position.x <= _drawLimits.x)
                {
                    toDestroy.Add(ch);
                }
        }
        foreach(Transform ch in toDestroy)
        {
            _chunks.Remove(ch);
            Destroy(ch.gameObject);
        }
    }


    void stopScroll()
    {
        _speed = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(_drawLimits.x, -5, 0), new Vector3(_drawLimits.x, 5, 0));
        Gizmos.DrawLine(new Vector3(_drawLimits.y, -5, 0), new Vector3(_drawLimits.y, 5, 0));
    }
}
