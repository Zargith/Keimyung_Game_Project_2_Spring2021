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

    public EnvironmentPosition _pos { get; private set; }

    public GameObject _prefab { get; private set; }

    public GameObject _instance;
    public EnvironmentVirus(Type type, GameObject prefab)
    {
        this.type = type;
        _prefab = prefab;
    }

    public void InitPosition(EnvironmentPosition pos)
    {
        _pos = pos;
    }

    /*public void Instanciat(EnvironmentPosition pos)
    {
        _pos = pos;
        _instance = Instantiate(_prefab, pos.position, pos.rotation);
    }*/

    public void Move(EnvironmentPosition pos)
    {
        _pos = pos;
        _instance.GetComponent<Transform>().SetPositionAndRotation(pos.position, pos.rotation);
    }
}
