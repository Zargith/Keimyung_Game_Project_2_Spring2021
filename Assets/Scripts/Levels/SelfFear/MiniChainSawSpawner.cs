using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniChainSawSpawner : MonoBehaviour
{
	[SerializeField] SelfFearGameManager selfFearGameManager;
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
			GameObject miniChainSaw = Instantiate(prefabBlock, this.transform);
			miniChainSaw.GetComponent<OnTriggerEnterResetGame>().selfFearGameManager = selfFearGameManager;
			timer = frequencyOfOccurrence;
		}
	}

	IEnumerator SpawnBlocks()
	{
		yield return new WaitForSeconds(frequencyOfOccurrence);
	}
}
