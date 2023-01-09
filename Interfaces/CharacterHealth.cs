namespace Hathor
{
    public interface ICharacterHealthBuffer
    {
        int Value { get; }

        // 提升数值
        void Increase(int value);

        // 扩大数值
        void Expend(float value);
    }

    public interface ICharacterHealth
    {
        // 血量值
        int Value { get; }

        // 回复计算
        ICharacterHealthBuffer GetHealingBuffer(string series);

        // 伤害计算
        ICharacterHealthBuffer GetDamageBuffer(string series);
    }
}
