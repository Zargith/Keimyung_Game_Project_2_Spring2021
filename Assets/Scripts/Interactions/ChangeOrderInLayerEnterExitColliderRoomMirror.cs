using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderInLayerEnterExitColliderRoomMirror : MonoBehaviour
{
	[SerializeField] SpriteRenderer mirrorSpriteRenderer;
	[SerializeField] int mirrorPositionInLayerWhenEnter = -1;
	[SerializeField] int mirrorPositionInLayerWhenExit = 0;
	[SerializeField] SpriteRenderer dresserSpriteRenderer;
	[SerializeField] int dresserPositionInLayerWhenEnter = 0;
	[SerializeField] int dresserPositionInLayerWhenExit = 1;
	[SerializeField] SpriteRenderer buttonSpriteRenderer;
	[SerializeField] int buttonPositionInLayerWhenEnter = 0;
	[SerializeField] int buttonPositionInLayerWhenExit = 1;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			buttonSpriteRenderer.sortingOrder = buttonPositionInLayerWhenEnter;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenEnter;
			mirrorSpriteRenderer.sortingOrder = mirrorPositionInLayerWhenEnter;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			buttonSpriteRenderer.sortingOrder = buttonPositionInLayerWhenExit;
			dresserSpriteRenderer.sortingOrder = dresserPositionInLayerWhenExit;
			mirrorSpriteRenderer.sortingOrder = mirrorPositionInLayerWhenExit;
		}
	}
}
