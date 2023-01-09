namespace Hathor
{
    public interface IAttributeChange
    {
        // 源属性
        IAttribute Source { get; }

        // 增益后数值
        int Value { get; }

        // 原始数值
        int OriginValue { get; }

        // 源属性的原始数值
        int SourceOriginValue { get; }

        // 消除增益
        void Dispel();
    }

    public interface IAttribute
    {
        // 属性数值
        int Value { get; }

        // 原始数值
        int OriginValue { get; set; }

        // 提升属性
        IAttributeChange Increase(int value);

        // 扩大属性
        IAttributeChange Expand(float value);

        // 扩大提升的属性
        IAttributeChange ExpendIncrease(float value);
    }
}
