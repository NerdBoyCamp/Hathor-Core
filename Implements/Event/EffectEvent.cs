namespace Hathor
{
    class EffectEvent : IEvent
    {
        public IEffect Effect;

        public string Series { get => "effect"; }

        public override string ToString() {
            return "effect applied";
        }
    }
}
