using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSkin : MonoBehaviour
{

    [SerializeReference] Spine.Unity.SkeletonMecanim skeleton;
    [SerializeField] Spine.Unity.SkeletonDataAsset nskin;
    [SerializeField] GameObject player;
    [SerializeField] GameObject other;
    [SerializeField] FollowPlayer scriptCamra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.SetActive(false);
            other.SetActive(true);
            scriptCamra._playerTransform = other.transform;
            // skeleton.skeletonDataAsset = nskin;
            // skeleton.initialSkinName = "YourNewSkinName";
            // skeleton.Initialize(true);
        }
    }
}
