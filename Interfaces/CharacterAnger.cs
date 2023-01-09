namespace Hathor
{
    public interface ICharacterAnger
    {
        // 怒气值
        int Value { get; }

        // 消费
        bool Consume(int value);

        // 回复
        bool Recover(int value);
    }
}
