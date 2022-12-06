namespace Hathor
{
    public interface IEventListener
    {
        void OnNotify(IEvent ev);
    }

    public interface IEvent
    {
        // 事件类别
        string Series { get; }
    }
}
