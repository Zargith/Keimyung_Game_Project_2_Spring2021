using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelTriggerEnd : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private List<GameObject> map;
    [SerializeField] private GameObject block; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.x > map[0].transform.position.x && map.Count < 2)
        {
            var n_block = Instantiate(block);
            n_block.transform.position = new Vector3(map[0].transform.position.x + 98, 0, 0);
            map.Add(n_block);
        }
        if (map.Count > 1 && playerPos.position.x > map[1].transform.position.x + 70)
        {
            var tmp = map[0];
            map.Remove(tmp);
            GameObject.Destroy(tmp);
            print(map.Count);
        }
    }
}
