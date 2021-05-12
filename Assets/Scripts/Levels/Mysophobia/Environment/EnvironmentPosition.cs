using UnityEngine;

public class EnvironmentPosition 
{
    public enum Placeholder
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public Placeholder placeholder { get; private set; }

    public Vector2 position { get; private set; }

    public Quaternion rotation { get; private set; }

    public EnvironmentPosition(Placeholder placeholder, Vector2 position, Quaternion rotation)
    {
        this.placeholder = placeholder;
        this.position = position;
        this.rotation = rotation;
    }
}
