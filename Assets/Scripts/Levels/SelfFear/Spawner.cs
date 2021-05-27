using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject prefabBlock;
	[SerializeField] float frequencyOfOccurrence = 1.0f;
	float timer = 0f;

	void Start()
	{
		timer = frequencyOfOccurrence;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			Instantiate(prefabBlock, this.transform);
			timer = frequencyOfOccurrence;
		}

	}

	IEnumerator SpawnBlocks()
	{
		yield return new WaitForSeconds(frequencyOfOccurrence);
	}
}
