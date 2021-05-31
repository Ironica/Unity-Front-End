
public class PlatformSerialized
{
    public Coordinates coo { get; }
    public int level { get; }

    public PlatformSerialized(Coordinates coo, int level)
    {
        this.coo = coo;
        this.level = level;
    }
}