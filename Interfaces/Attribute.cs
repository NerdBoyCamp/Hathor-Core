namespace Hathor
{
    public interface IAttributeChange
    {
        // 源属性
        IAttribute Source { get; }

        // 增益后数值
        float Value { get; }

        // 原始数值
        float OriginValue { get; }

        // 源属性的原始数值
        float SourceOriginValue { get; }

        // 消除增益
        void Dispel();
    }

    public interface IAttribute
    {
        // 属性数值
        float Value { get; }

        // 原始数值
        float OriginValue { get; set; }

        // 提升属性
        IAttributeChange Increase(float value);

        // 扩大属性
        IAttributeChange Expand(float value);

        // 扩大提升的属性
        IAttributeChange ExpendIncrease(float value);
    }
}
