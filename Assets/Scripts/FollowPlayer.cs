using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] Transform _playerTransform;
	[SerializeField] float changeY = 0f;


	void Update()
	{
		if (_playerTransform == null)
			return;
		Vector3 tmp = new Vector3(_playerTransform.position.x, _playerTransform.position.y + changeY, transform.position.z);
		if (transform.position != tmp)
			transform.position = tmp;
	}

	public void SetTarget(Transform target)
	{
		_playerTransform = target;
	}

	public void SetY(float y)
	{
		changeY = y;
	}
}