
using JetBrains.Annotations;

public class LockSerialized
{
    public Coordinates coo;

    [CanBeNull] public Coordinates[] controlled;

    public bool? isActive;
    public int? energy;

    public LockSerialized(Coordinates coo, Coordinates[] controlled){
        this.coo = coo;
        this.controlled = controlled;
    }

    public LockSerialized(Coordinates coo, bool isActive, int energy)
    {
        this.coo = coo;
        this.isActive = isActive;
        this.energy = energy;
    }
}
