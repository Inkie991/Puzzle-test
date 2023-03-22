using UnityEngine;

public enum BorderShape
{
    TriangleInside,
    TriangleOutside,
    Flat
}

public enum BorderColor
{
    Red,
    Blue,
    Green,
    Yellow,
    None
}

public enum BorderSide
{
    Left,
    Top,
    Right,
    Bottom
}

public struct SlotId
{
    public int Row;
    public int Coll;
}

public struct BorderInfo
{
    public BorderSide Side;
    public BorderShape Shape;
    public BorderColor Color;
}

public static class BorderManager
{
    public static Color GetColor(BorderColor color)
    {
        switch (color)
        {
            case BorderColor.Red:
                return Color.red;
            case BorderColor.Green:
                return Color.green;
            case BorderColor.Blue:
                return Color.blue;
            case BorderColor.Yellow:
                return Color.yellow;
            case BorderColor.None:
                return Color.white;
        }
        return Color.white;
    }

    public static BorderShape GetOppositeShape(BorderShape shape)
    {
        switch (shape)
        {
            case BorderShape.TriangleOutside:
                return BorderShape.TriangleInside;
            case BorderShape.TriangleInside:
                return BorderShape.TriangleOutside;
            case BorderShape.Flat:
                return BorderShape.Flat;
        }
        return BorderShape.Flat;
    }
}