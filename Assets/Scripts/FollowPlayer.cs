using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] Transform _playerTransform;
	[SerializeField] float changeY = 0f;


	void Update()
	{
		Vector3 tmp = new Vector3(_playerTransform.position.x, _playerTransform.position.y + changeY, transform.position.z);
		if (transform.position != tmp)
			transform.position = tmp;
	}
}