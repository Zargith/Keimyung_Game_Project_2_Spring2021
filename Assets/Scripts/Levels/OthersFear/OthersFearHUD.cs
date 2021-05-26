using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OthersFearHUD : MonoBehaviour
{
    [SerializeField] OthersFearPlayer player;
    [SerializeField] List<OthersFear_Item> inventory;
    private Camera cam;
    private Vector3 caseSize;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        foreach (var item in inventory)
        {
            item.gameObject.SetActive(false);
            Sprite s = item.gameObject.GetComponent<SpriteRenderer>().sprite;
            caseSize = new Vector3(Mathf.Max(caseSize.x, s.texture.width / s.pixelsPerUnit), Mathf.Max(caseSize.y, s.texture.height / s.pixelsPerUnit), caseSize.z);
        }
        print(caseSize);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = 0.0f;
        foreach (OthersFear_Item item in inventory)
        {
            int amount = player.inventory.FindAll(delegate (OthersFear_Item.EnumOthersFearItemType type) { return type.Equals(item.Item); }).Count;
            if (amount == 0)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
                item.transform.localPosition = new Vector3(-cam.orthographicSize * cam.aspect + caseSize.x * 0.6f + pos, cam.orthographicSize - caseSize.y * 0.6f, 10);
                item.gameObject.GetComponentInChildren<UnityEngine.UI.Text>(true).text = amount.ToString();
                pos += caseSize.x * 1.5f;
            }
        }
    }
}
