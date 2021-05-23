using UnityEngine;

public class PositionProvider
{
    public enum Side
    {
        X,
        Y
    }
    public Vector2 Origin { get; private set; }

    public Vector2 Middle { get; private set; }

    public Vector2Int MapSize { get; private set; }

    public float SquareSize { get; private set; }

    private PositionProvider() { }

    public PositionProvider(Vector2 origin, Vector2Int mapSize, float squareSize)
    {
        Origin = origin;
        MapSize = mapSize;
        SquareSize = squareSize;
        Middle = Origin + new Vector2(mapSize.x * squareSize / 2, mapSize.y * squareSize / 2);
    }

    public float GetSideLength(Side side)
    {
        return SquareSize * (side == Side.X ? MapSize.x : MapSize.y);
    }

    public Vector2 GetSidePointRelativeToMiddle(Side side, bool min = false)
    {
        Vector2 tmp;

        if (side == Side.X)
        {
            tmp = new Vector2(GetSideLength(side) / 2, 0);
        }
        else
        {
            tmp = new Vector2(0, GetSideLength(side) / 2);
        }
        return (min == true ? Middle - tmp : Middle + tmp);
    }
}
