
public class GridString
{
    public string block { get; }
    public Biome biome { get; }
    public int level { get; }

    public GridString(string block, Biome biome, int level)
    {
        this.block = block;
        this.biome = biome;
        this.level = level;
    }
}
