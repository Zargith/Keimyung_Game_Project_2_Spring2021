using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersFearPnj : MonoBehaviour
{
    [SerializeField] OthersFear_Item.EnumOthersFearItemType need;
    [SerializeField] OthersFearEnemy distract;
    [SerializeField] GameObject trigger;
    bool distracting = false;
    bool move_to_target = false;
    public float speed = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TextMesh>(true).text = "Hi ! If you have a " + need.ToString() + " I can help you";
    }

    // Update is called once per frame
    void Update()
    {
        if (distracting)
        {
            distract.Distracted(true);
            GetComponentInChildren<PolygonCollider2D>(true).enabled = false;
            return;
        }
        else if (move_to_target)
        {
            transform.position += (distract.GetDistractPos() - transform.position).normalized * speed * Time.deltaTime;
            if ((transform.position - distract.GetDistractPos()).magnitude < 0.01)
            {
                move_to_target = false;
                distracting = true;
                GetComponentInChildren<TextMesh>(true).text = "~~~~~~  ~~~~ ~~~  ~~~~~~~~~~~~";
                GetComponentInChildren<PolygonCollider2D>(true).enabled = false;
            }
        }
        else
        {
            if (trigger.activeSelf && Input.GetButtonDown("Interact") && FindObjectOfType<OthersFearPlayer>().inventory.Remove(need))
            {
                move_to_target = true;
            }
        }
    }
}
