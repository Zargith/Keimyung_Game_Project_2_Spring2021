using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeReference] GameObject reward;
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
            reward.transform.localPosition = new Vector3(reward.transform.localPosition.x, -4.5f, 0);
            var tmp = reward.GetComponentInChildren<OthersFear_Item>().gameObject;
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = 1.25f;
        }
    }
}
