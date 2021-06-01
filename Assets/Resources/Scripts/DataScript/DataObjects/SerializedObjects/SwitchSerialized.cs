namespace JsonBridge
{
    public class SwitchSerialized
    {
        public Coordinates coo { get; }
        public bool on { get; }

        public SwitchSerialized(Coordinates coo, bool on)
        {
            this.coo = coo;
            this.on = on;
        }
    }
}