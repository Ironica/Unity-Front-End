
using System;

public class Switch
{
    
    public int X { get; }
    
    public int Y { get; }
    public bool On { get; }

    public Switch(int x, int y, bool on)
    {
        this.X = x;
        this.Y = y;
        this.On = on;
    }
}