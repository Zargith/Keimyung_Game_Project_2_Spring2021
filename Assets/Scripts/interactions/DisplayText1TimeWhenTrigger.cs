using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText1TimeWhenTrigger : MonoBehaviour
{
    MeshRenderer mesh;
    bool hasAlreadyTriggered = false;
    TextMesh text;
    float alpha = 0;

    void Start() {
        mesh = GetComponent<MeshRenderer>();
        text = GetComponent<TextMesh>();
        var matrialColor = text.color;
        text.color = new Color(matrialColor.r, matrialColor.g, matrialColor.b, 0);
    }

    void Update()
    {
        if (mesh.enabled && !hasAlreadyTriggered && alpha < 100)
            alpha++;
        else if (mesh.enabled && hasAlreadyTriggered) {
            if (alpha > 0)
                alpha--;
            else
                mesh.enabled = false;
        }

        var color = text.color;
        text.color = new Color(color.r, color.g, color.b, alpha / 100);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasAlreadyTriggered && other.gameObject.CompareTag("Player"))
            mesh.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (!hasAlreadyTriggered && other.gameObject.CompareTag("Player"))
            hasAlreadyTriggered = true;
    }
}
