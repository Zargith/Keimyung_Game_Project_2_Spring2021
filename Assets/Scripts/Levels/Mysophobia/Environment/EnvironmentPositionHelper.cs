using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;

public class EnvironmentPositionHelper
{
    private Dictionary<Placeholder, Vector2> _positions;

    public EnvironmentPositionHelper(PositionProvider pp)
    {

        Dictionary<Placeholder, Vector2> tempPositions = new Dictionary<Placeholder, Vector2> {
            { Placeholder.EAST, pp.GetSidePointRelativeToMiddle(PositionProvider.Side.X) },
            { Placeholder.NORTH, pp.GetSidePointRelativeToMiddle(PositionProvider.Side.Y) },
            { Placeholder.WEST, pp.GetSidePointRelativeToMiddle(PositionProvider.Side.X, true) },
            { Placeholder.SOUTH, pp.GetSidePointRelativeToMiddle(PositionProvider.Side.Y, true) }
        };
        AddOffset(pp, tempPositions);
    }

    public Vector2 GetPosition(Placeholder placeholder)
    {
        return (_positions[placeholder]);
    }

    private void AddOffset(PositionProvider pp, Dictionary<Placeholder, Vector2> tempPositions)
    {
        Vector2 temp;

        _positions = new Dictionary<Placeholder, Vector2>();
        foreach (KeyValuePair<Placeholder, Vector2> kvp in tempPositions)
        {
            temp = kvp.Value;
            switch (kvp.Key)
            {
                case Placeholder.EAST:
                    temp.x += pp.GetSideLength(PositionProvider.Side.X) / 4 - pp.SquareSize;
                    _positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.NORTH:
                    temp.y += pp.GetSideLength(PositionProvider.Side.Y) / 4 - pp.SquareSize;
                    _positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.WEST:
                    temp.x -= pp.GetSideLength(PositionProvider.Side.X) / 4;
                    _positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.SOUTH:
                    temp.y -= pp.GetSideLength(PositionProvider.Side.Y) / 4;
                    _positions.Add(kvp.Key, temp);
                    break;
            }
        }
    }
}
