using UnityEngine;

public class EnvironmentVirus: ScriptableObject
{
    public enum Type
    {
        INSTALLER,
        CORONA,
        FLU,
        PLAGUE
    }

    public Type type { get; private set; }

    public EnvironmentPosition.Placeholder placeholder { get; private set; }

    private GameObject _prefab;
    public EnvironmentVirus(Type type, GameObject prefab)
    {
        this.type = type;
        _prefab = prefab;
    }

    public void Instanciat(EnvironmentPosition pos)
    {
        placeholder = pos.placeholder;
        Instantiate(_prefab, pos.position, pos.rotation);
    }
}
