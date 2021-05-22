using UnityEngine;

public abstract class PositionableGraphic : PrefabReceiver
{
    protected PositionProvider _pp;
    /*public PositionableGraphic(PositionProvider pp)
    {
        _pp = pp;
    }*/

    public abstract void Init(PositionProvider pp);

    public abstract void Draw();
}
