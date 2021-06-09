using JetBrains.Annotations;

namespace Resources.Scripts.DataScript.DataObjects.Rules
{
    public class GamingCondition
    {
        public int? collectGemBy { get; set; }
        public int? switchesOnBy { get; set; }
        [CanBeNull] public Coordinates[] arriveAt { get; set; }
        public int? monstersKilled { get; set; }
        public int? monstersKilledLessThan { get; set; }
        public bool? noSameTileRepassed { get; set; }
        public int? endGameAfter { get; set; }
    }
}