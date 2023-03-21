public enum BorderShape
    {
        TriangleInside,
        TriangleOutside,
        Flat
    }

public enum PuzzleColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        None

    }

public struct SlotId
{
    public int Row;
    public int Coll;
}

public struct BorderInfo
{
    public BorderShape Shape;
    public PuzzleColor Color;
}
