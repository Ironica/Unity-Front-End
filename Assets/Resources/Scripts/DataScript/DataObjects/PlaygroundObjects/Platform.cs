
using System;

public class Platform
{
    public int X { get; }
    public int Y { get; }
    public int Level { get; }

    public Platform(int x, int y, int level)
    {
        this.X = x;
        this.Y = y;
        this.Level = level;
    }
}