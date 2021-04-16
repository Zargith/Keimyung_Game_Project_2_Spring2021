using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float changeY = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = new Vector3(_player.transform.position.x, _player.transform.position.y + changeY, transform.position.z);
        if (transform.position != tmp)
            transform.position = tmp;
    }
}