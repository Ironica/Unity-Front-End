
public class GridObject
{
    public Block block { get; }
    public Biome biome { get; }
    public int level { get; }

    public GridObject(Block block, Biome biome, int level)
    {
        this.block = block;
        this.biome = biome;
        this.level = level;
    }
}
