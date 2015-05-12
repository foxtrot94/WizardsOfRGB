using UnityEngine;
using System.Collections;
using System;

public class GameColor {
    public enum Colors
    {
        Black = 0,

        // Primary colors
        Red = 1,
        Green = 2,
        Blue = 4,

        // 2-Colors combination
        Yellow = 3,
        Cyan = 6,
        Magenta = 5,

        // 3-Colors combination
        White = 7,
    }

    private static readonly int[] components = {
        0,
        1,
        1,
        2,
        1,
        2,
        2,
        3,
    };

    private static readonly Color[] map = new Color[] {
        Color.black,
        Color.red,
        Color.green,
        Color.yellow,
        new Color(0.25f, 0.25f, 1.00f), // Color.blue
        Color.magenta,
        Color.cyan,
        Color.white
    };

    private static Colors[][] sets = new Colors[][] {
        new Colors[] {Colors.Black},
        new Colors[] {Colors.Red, Colors.Green, Colors.Blue},
        new Colors[] {Colors.Yellow, Colors.Cyan, Colors.Magenta},
        new Colors[] {Colors.White}
    };

    public static int GetNumComponents(int color)
    {
        return components[color];
    }

    public static Color GetDisplayColor(int color)
    {
        return map[color];
    }

    public static int Combine(int colorA, int colorB)
    {
        return Math.Min(7, colorA + colorB);
    }

    public static bool Collides(int colorA, int colorB)
    {
        return colorA == colorB;
    }

    public static int Random(int numComponents)
    {
        return (int)sets[numComponents][UnityEngine.Random.Range(0, sets[numComponents].Length)];
    }
}