using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TutoOnExitColliderCloseDoor : MonoBehaviour
{
	AudioSource _audioSource;
	BoxCollider2D _boxCollider;
	[SerializeField] GameObject outsideDreamRoom;
	[SerializeField] SpriteRenderer fournituresToChangeOrderInLayer;
	[SerializeField] Light2D lightToStop;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && other.transform.position.x > transform.position.x) {
			_audioSource.Play();
			_boxCollider.isTrigger = false;
			outsideDreamRoom.SetActive(false);
			fournituresToChangeOrderInLayer.sortingOrder = 1;
			lightToStop.enabled = false;
		}
	}
}
