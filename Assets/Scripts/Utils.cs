using System;
using System.Collections;
using UnityEngine;

public enum ManagerStatus
{
    Shutdown,
    Initializing,
    Started
}

public enum BorderShape
{
    TriangleInside,
    TriangleOutside,
    Flat,
    None
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
    Bottom,
    None
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

public static class Utils
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
        return BorderShape.None;
    }

    public static BorderSide GetOppositeSide(BorderSide side)
    {
        switch (side)
        {
            case BorderSide.Left:
                return BorderSide.Right;
            case BorderSide.Top:
                return BorderSide.Bottom;
            case BorderSide.Right:
                return BorderSide.Left;
            case BorderSide.Bottom:
                return BorderSide.Top;
        }
        return BorderSide.None;
    }
    
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }
 
    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}