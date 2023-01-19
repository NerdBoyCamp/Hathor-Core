namespace Hathor
{
    class BattleDamageEvent : IEvent
    {
        public ICharacter Character = null;
        public float Damage = 0;
        public string Series { get => "battle"; }

        public override string ToString()
        {
            return "damage";
        }
    }
}
