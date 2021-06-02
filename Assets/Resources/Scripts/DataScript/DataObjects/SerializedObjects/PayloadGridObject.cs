

public class PayloadGridObject
{
    public Block block { get; }
    public int level { get; }

    public PayloadGridObject(Block block, int level)
    {
        this.block = block;
        this.level = level;
    }
}
