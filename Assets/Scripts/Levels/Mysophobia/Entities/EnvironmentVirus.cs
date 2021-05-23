using UnityEngine;
public class EnvironmentVirus
{
    public enum Type
    {
        INSTALLER,
        CORONA,
        FLU,
        PLAGUE
    }

    public Type type { get; private set; }

    public EnvironmentPosition Pos { get; private set; }

    public GameObject Prefab { get; private set; }

    public GameObject Instance;
    public EnvironmentVirus(Type type, GameObject prefab)
    {
        this.type = type;
        Prefab = prefab;
    }

    public void InitPosition(EnvironmentPosition pos)
    {
        Pos = pos;
    }

    public void Move(EnvironmentPosition pos)
    {
        Pos = pos;
        Instance.GetComponent<Transform>().SetPositionAndRotation(pos.Position, pos.Rotation);
    }
}
