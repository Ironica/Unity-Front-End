public class Portal
{
    public int X { get; }
    public int Y { get; }

    public bool isActive { get; }

    public Portal(int x, int y, bool isActive)
    {
        this.X = x;
        this.Y = y;
        this.isActive = isActive;
    }
}