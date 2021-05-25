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

    public Vector2 Position { get; private set; }

    public Quaternion Rotation { get; private set; }

    public EnvironmentPosition(Placeholder placeholder, Vector2 position, Quaternion rotation)
    {
        this.placeholder = placeholder;
        Position = position;
        Rotation = rotation;
    }
}
