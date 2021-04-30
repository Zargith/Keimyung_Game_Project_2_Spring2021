using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoOnExitColliderCloseDoor : MonoBehaviour
{
	AudioSource _audioSource;
	BoxCollider2D _boxCollider;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && other.transform.position.x > transform.position.x) {
			_audioSource.Play();
			_boxCollider.isTrigger = false;
		}
	}
}
