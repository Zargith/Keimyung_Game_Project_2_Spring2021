using UnityEngine;
using UnityEngine.UI;

public abstract class Displayable<T, U>
{
    protected T _object;

    public Displayable(string handlerName)
    {
        _object = GameObject.Find(handlerName).GetComponent<T>();
    }
    public abstract void Update(U value);
}

public class TextDisplayable : Displayable<Text, string>
{
    public TextDisplayable(string handlerName) : base(handlerName) { }
    public override void Update(string value)
    {
        _object.text = value;
    }
}