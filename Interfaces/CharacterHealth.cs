namespace Hathor
{
    public interface ICharacterHealthBuffer
    {
        float Value { get; }

        // 提升数值
        void Increase(float value);

        // 扩大数值
        void Expend(float value);
    }

    public interface ICharacterHealth
    {
        // 血量值
        float Value { get; }

        // 回复计算
        ICharacterHealthBuffer GetHealingBuffer(string series);

        // 伤害计算
        ICharacterHealthBuffer GetDamageBuffer(string series);
    }
}
