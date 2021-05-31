using UnityEngine;
using UnityEngine.UI;

public class ListCreator : MonoBehaviour
{
    private LevelManager _manager;

    [SerializeField]
    private GameObject Menu = null;

    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    public void Start()
    {
        _manager = GameObject.Find("Root").GetComponent<LevelManager>();
    }

    // Use this for initialization
    public void Draw(string[] texts, string[] paths)
    {
        content.sizeDelta = new Vector2(0, texts.Length * 60);
        int[] indexes = new int[texts.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            Vector3 pos = new Vector3(SpawnPoint.position.x, -(i * 60), SpawnPoint.position.z);
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            Button button = SpawnedItem.GetComponentInChildren<Button>();
            Text text = SpawnedItem.GetComponentInChildren<Text>();
            string path = paths[i];
            button.onClick.AddListener(DeactivateMenu);
            button.onClick.AddListener(() => { LoadLevel(path); });
            text.text = texts[i];
        }
    }

    private void DeactivateMenu()
    {
        Menu.SetActive(false);
    }

    private void LoadLevel(string path)
    {
        _manager.LoadFreeplayMap(path);
    }
}
