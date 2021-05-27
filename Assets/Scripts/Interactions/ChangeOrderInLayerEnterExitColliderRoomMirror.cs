using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderInLayerEnterExitColliderRoomMirror : MonoBehaviour
{
	[SerializeField] SpriteRenderer mirrorSpriteRenderer;
	[SerializeField] int mirrorPositionInLayerWhenEnter = -1;
	[SerializeField] int mirrorPositionInLayerWhenExit = -4;
	[SerializeField] SpriteRenderer dresserSpriteRenderer;
	[SerializeField] int dresserPositionInLayerWhenEnter = 0;
	[SerializeField] int dresserPositionInLayerWhenExit = 1;
	[SerializeField] SpriteRenderer buttonSpriteRenderer;
	[SerializeField] int buttonPositionInLayerWhenEnter = 0;
	[SerializeField] int buttonPositionInLayerWhenExit = -3;
	[SerializeField] SpriteRenderer gelSpriteRenderer;
	[SerializeField] int gelPositionInLayerWhenEnter = 1;
	[SerializeField] int gelPositionInLayerWhenExit = -2;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			buttonSpriteRenderer.sortingOrder = buttonPositionInLayerWhenEnter;
			gelSpriteRenderer.sortingOrder = gelPositionInLayerWhenEnter;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenEnter;
			mirrorSpriteRenderer.sortingOrder = mirrorPositionInLayerWhenEnter;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			buttonSpriteRenderer.sortingOrder = buttonPositionInLayerWhenExit;
			gelSpriteRenderer.sortingOrder = gelPositionInLayerWhenExit;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenExit;
			mirrorSpriteRenderer.sortingOrder = mirrorPositionInLayerWhenExit;
		}
	}
}
