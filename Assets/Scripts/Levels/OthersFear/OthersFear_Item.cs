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
    [SerializeField] GameObject btn;
    [SerializeField] List<Sprite> item_sprites;

    public EnumOthersFearItemType Item { get => item; }

    // Start is called before the first frame update
    void Start()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();
        if (item != EnumOthersFearItemType.SIZE)
        {
            spr.sprite = item_sprites[(int)item];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (btn && btn.activeSelf)
            {
                FindObjectOfType<OthersFearPlayer>().inventory.Add(item);
                GameObject.Destroy(gameObject);
            }
        }
    }
}
