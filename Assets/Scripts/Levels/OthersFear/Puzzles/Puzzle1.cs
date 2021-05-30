using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeReference] GameObject reward;
    [SerializeField] Vector2 posReward;
    [SerializeReference] List<Puzzle1_Lever> triggers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(reward))
        {
            foreach (var item in triggers)
            {
                item.enabled = false;
            }
            reward.transform.rotation = Quaternion.Euler(0, 0, 0);
            reward.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            reward.GetComponent<Collider2D>().enabled = false;
            reward.transform.position = posReward;
            var tmp = reward.GetComponentInChildren<OthersFear_Item>().gameObject;
            tmp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            tmp.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = 1.25f;
        }
    }
}
