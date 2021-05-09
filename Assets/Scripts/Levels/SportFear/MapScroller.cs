using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _savedSpeed;
    [SerializeField] Vector2 _drawLimits;
    [SerializeField] List<Transform> _chunks = new List<Transform>();

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

    private void Start()
    {
        foreach(Transform child in transform)
        {
            _chunks.Add(child);
        }
    }
    
    void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
        foreach (Transform ch in _chunks)
        {
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
                    ch.gameObject.SetActive(false);
                }
        }
    }


    public void stop()
    {
        _savedSpeed = _speed == 0 ? _savedSpeed : _speed;
        _speed = 0;
    }

    public void restart()
    {
        transform.position = Vector3.zero;
        _speed = _savedSpeed;
    }


    public void addSpeed(float s)
    {
        _speed += s;
    }

    public void removeFromChunk(Transform ch)
    {
        _chunks.Remove(ch);
        Destroy(ch.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(_drawLimits.x, -5, 0), new Vector3(_drawLimits.x, 5, 0));
        Gizmos.DrawLine(new Vector3(_drawLimits.y, -5, 0), new Vector3(_drawLimits.y, 5, 0));
    }

}
