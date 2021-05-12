using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;

public class EnvironmentPositionHelper
{
    private Dictionary<Placeholder, Vector2> positions;

    public EnvironmentPositionHelper(PositionProvider pp)
    {

        Dictionary<Placeholder, Vector2> tempPositions = new Dictionary<Placeholder, Vector2> {
            { Placeholder.EAST, pp.getSidePointRelativeToMiddle(PositionProvider.Side.X) },
            { Placeholder.NORTH, pp.getSidePointRelativeToMiddle(PositionProvider.Side.Y) },
            { Placeholder.WEST, pp.getSidePointRelativeToMiddle(PositionProvider.Side.X, true) },
            { Placeholder.SOUTH, pp.getSidePointRelativeToMiddle(PositionProvider.Side.Y, true) }
        };
        addOffset(pp, tempPositions);
    }

    public Vector2 getPosition(Placeholder placeholder)
    {
        return (positions[placeholder]);
    }

    private void addOffset(PositionProvider pp, Dictionary<Placeholder, Vector2> tempPositions)
    {
        Vector2 temp;

        positions = new Dictionary<Placeholder, Vector2>();
        foreach (KeyValuePair<Placeholder, Vector2> kvp in tempPositions)
        {
            temp = kvp.Value;
            switch (kvp.Key)
            {
                case Placeholder.EAST:
                    temp.x += pp.getSideLength(PositionProvider.Side.X) / 4 - pp.SquareSize;
                    positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.NORTH:
                    temp.y += pp.getSideLength(PositionProvider.Side.Y) / 4 - pp.SquareSize;
                    positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.WEST:
                    temp.x -= pp.getSideLength(PositionProvider.Side.X) / 4;
                    positions.Add(kvp.Key, temp);
                    break;
                case Placeholder.SOUTH:
                    temp.y -= pp.getSideLength(PositionProvider.Side.Y) / 4;
                    positions.Add(kvp.Key, temp);
                    break;
            }
        }
    }
}
