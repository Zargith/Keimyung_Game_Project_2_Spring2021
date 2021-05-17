using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTouch : MonoBehaviour
{
	[SerializeField] string sceneName;
    bool trigered = false;
    float t = 0;
    SpriteRenderer sp;

    private void Update()
    {
        if (trigered)
        {
            t += Time.deltaTime;
            sp.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, t);
            if (t > 1)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trigered = true;
            sp = GameObject.Find("Hider").GetComponent<SpriteRenderer>();
        }
    }
}
