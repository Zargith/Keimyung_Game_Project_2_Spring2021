using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderInLayerEnterExitColliderRoomDresser : MonoBehaviour
{
	[SerializeField] SpriteRenderer dresserSpriteRenderer;
	[SerializeField] int dresserPositionInLayerWhenEnter = 0;
	[SerializeField] int dresserPositionInLayerWhenExit = 1;
	[SerializeField] SpriteRenderer gelSpriteRenderer;
	[SerializeField] int gelPositionInLayerWhenEnter = 0;
	[SerializeField] int gelPositionInLayerWhenExit = 1;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			gelSpriteRenderer.sortingOrder = gelPositionInLayerWhenEnter;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenEnter;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			gelSpriteRenderer.sortingOrder = gelPositionInLayerWhenExit;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenExit;
		}
	}
}
