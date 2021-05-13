using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersFear_Item : MonoBehaviour
{
    public enum EnumOthersFearItemType
    {
        SMARS,
        HARIBUS,
        SIZE,
    }

    [SerializeField] EnumOthersFearItemType item;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject btn;

    private EnumOthersFearItemType Item { get => item;}

    // Start is called before the first frame update
    void Start()
    {
        SetSprite();   
    }

    private void SetSprite()
    {
        SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();
        if (item != EnumOthersFearItemType.SIZE)
            spr.sprite = sprites[(int)item];
    }

    // Update is called once per frame
    void Update()
    {
        if (btn.activeSelf && Input.GetButtonDown("Interact"))
        {
            FindObjectOfType<OthersFearPlayer>().inventory.Add(item);
        }
    }
}
