using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderInLayerEnterExitCollider : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] int positionInLayerWhenEnter = -1;
	[SerializeField] int positionInLayerWhenExit = 0;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			spriteRenderer.sortingOrder = positionInLayerWhenEnter;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			spriteRenderer.sortingOrder = positionInLayerWhenExit;
	}
}
