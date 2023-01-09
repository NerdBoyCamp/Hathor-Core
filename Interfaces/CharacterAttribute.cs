namespace Hathor
{
    public interface ICharacterAttributeChange
    {
        // 源属性
        ICharacterAttribute Source { get; }

        // 增益后数值
        int Value { get; }

        // 原始数值
        int OriginValue { get; }

        // 源属性的原始数值
        int SourceOriginValue { get; }

        // 消除增益
        void Dispel();
    }

    public interface ICharacterAttribute
    {
        // 属性数值
        int Value { get; }

        // 原始数值
        int OriginValue { get; set; }

        // 提升属性
        ICharacterAttributeChange Increase(int value);

        // 扩大属性
        ICharacterAttributeChange Expand(float value);

        // 扩大提升的属性
        ICharacterAttributeChange ExpendIncrease(float value);
    }
}
