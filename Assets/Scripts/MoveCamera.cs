using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.75f, -10);
        if (transform.position != tmp)
            transform.position = tmp;
    }
}