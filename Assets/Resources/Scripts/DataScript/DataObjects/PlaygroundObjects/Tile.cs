
using System;

public class Tile
{
    public Block Block { get; }
    public Biome Biome { get; }
    public int Level { get; }

    public Tile(Block block, Biome biome, int level)
    {
        this.Block = block;
        this.Biome = biome;
        this.Level = level;
    }
}
