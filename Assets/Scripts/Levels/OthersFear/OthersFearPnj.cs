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
        if (need != OthersFear_Item.EnumOthersFearItemType.SIZE)
            GetComponentInChildren<TextMesh>(true).text = "Hi ! If you have a " + need.ToString() + " I can help you";
        else
            GetComponentInChildren<DisplayIfPlayerIsInZone>(true).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (distracting)
        {
            distract.Distracted(true);
        }
        else if (move_to_target)
        {
            print((transform.position - distract.GetDistractPos()).magnitude);
            transform.position += (distract.GetDistractPos() - transform.position).normalized * speed * Time.deltaTime;
            if ((transform.position - distract.GetDistractPos()).magnitude < 0.05)
            {
                move_to_target = false;
                distracting = true;
                GetComponentInChildren<TextMesh>(true).text = "~~~~~~  ~~~~ ~~~  ~~~~~~~~~~~~";
                GetComponentInChildren<PolygonCollider2D>(true).gameObject.SetActive(false);
            }
        }
        else
        {
            if (trigger.activeSelf && Input.GetButtonDown("Interact") && FindObjectOfType<OthersFearPlayer>().inventory.Remove(need))
            {
                move_to_target = true;
                GetComponentInChildren<TextMesh>(true).text = "";
                GetComponentInChildren<DisplayIfPlayerIsInZone>(true).gameObject.SetActive(false);
                trigger.SetActive(false);
            }
        }
    }
}
