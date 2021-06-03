using Resources.Scripts.DataScript.DataObjects;

namespace JsonBridge
{
    
    // This is the data structure for the grid in RealDataOutSerialized
    // Notice that we use BlockData (only OPEN and BLOCKED) instead of Block
    public class OutgoingGridObject
    {
        public BlockData block { get; }
        public Biome biome { get; }
        public int level { get; }

        public OutgoingGridObject(BlockData block, Biome biome, int level)
        {
            this.block = block;
            this.biome = biome;
            this.level = level;
        }
    }
}