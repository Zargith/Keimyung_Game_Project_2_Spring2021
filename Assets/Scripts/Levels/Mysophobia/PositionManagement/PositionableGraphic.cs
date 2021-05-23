public abstract class PositionableGraphic : PrefabReceiver
{
    protected PositionProvider _pp;

    public abstract void Init(PositionProvider pp);

    public abstract void Draw();
}
