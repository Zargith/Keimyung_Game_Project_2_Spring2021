using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager
{
    public Dictionary<string, TextDisplayable> Texts { get; private set; }
    public DisplayManager()
    {
        Texts = new Dictionary<string, TextDisplayable>();
    }

    public void AddText(string handlerName, string keyName = null)
    {
        if (keyName == null)
            Texts.Add(handlerName, new TextDisplayable(handlerName));
        else
            Texts.Add(keyName, new TextDisplayable(handlerName));
    }
}
