using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel_MapSetup : MonoBehaviour
{
    [SerializeField] Vector3 playerStart;
    [SerializeField] List<Sprite> tree_types;
    [SerializeField] List<Sprite> leaves_types;
    [SerializeField] float mapLenght = 100;
    [SerializeField] float treeSpacing = 5f;
    [SerializeField] AnimationCurve treeSpacingStrength;

    // Start is called before the first frame update
    void Start()
    {
        SetGround();
        SetTrees();
    }

    private void SetGround()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.gameObject.name == "grass_ground")
            {
                SetFloor(item.gameObject);
            }
        }

    }

    private void SetFloor(GameObject ground)
    {
        ground.transform.localPosition = new Vector3(mapLenght / 2f, ground.transform.localPosition.y, 0);
        SpriteRenderer sr = ground.GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
        sr.size = new Vector2(mapLenght / ground.transform.localScale.x, sr.size.y);
        foreach (var item in ground.GetComponentsInChildren<Transform>())
        {
            if (item.name == "under_grass")
                item.localScale = new Vector3(sr.size.x, item.localScale.y, item.localScale.z);
        }

    }

    private void SetTrees()
    {
        float pos_x = 0f;
        GameObject forest = new GameObject("Forest");

        forest.transform.parent = transform;
        forest.transform.localPosition = new Vector3();
        while (pos_x < mapLenght)
        {
            GameObject trunk = new GameObject("Tree");
            trunk.transform.parent = forest.transform;
            trunk.transform.localScale = new Vector3((Random.Range(0, 2) == 0) ? -1 : 1, 1, 1);
            SpriteRenderer sp = trunk.AddComponent<SpriteRenderer>();
            sp.sprite = tree_types[Random.Range(0, tree_types.Count)];
            var order = Random.Range(2, 10);
            sp.sortingOrder = order;
            GameObject leaves = new GameObject("Leaves");
            leaves.transform.parent = trunk.transform;
            sp = leaves.AddComponent<SpriteRenderer>();
            sp.sprite = leaves_types[Random.Range(0, leaves_types.Count)];
            sp.sortingOrder = order + 1;
            leaves.AddComponent<Recoloring>();
            leaves.AddComponent<BoxCollider2D>().isTrigger = true;
            PlaceTree(trunk, treeSpacing / 10f + pos_x, leaves);
            var delta = Mathf.Max(1f, treeSpacing * treeSpacingStrength.Evaluate((float)pos_x / (float)mapLenght));
            pos_x += delta;
        }
    }

    private void PlaceTree(GameObject tree, float x, GameObject leaves)
    {
        Sprite sprite = tree.GetComponent<SpriteRenderer>().sprite;
        tree.transform.localPosition = new Vector3(x + Random.Range(-treeSpacing / 10f, treeSpacing / 10f), sprite.textureRect.height / 2 / sprite.pixelsPerUnit, 0);
        leaves.transform.localPosition = new Vector3(0, sprite.textureRect.height / 2 / sprite.pixelsPerUnit * Random.Range(0.2f, 0.5f), 0);
        leaves.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }
}
