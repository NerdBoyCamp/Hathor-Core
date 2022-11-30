namespace Hathor
{
    public interface IEvent
    {
        string ID { get; }
    }

    public interface IEventBattle : IEvent { }
}
