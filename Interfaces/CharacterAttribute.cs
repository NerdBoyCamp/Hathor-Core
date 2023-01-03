namespace Hathor
{
    public interface ICharacterAttribute
    {
        // 属性数值
        int Value { get; }

        // 降低属性
        void Decrease(int value);

        // 降低属性的增幅
        void DecreaseAmplify(float value);

        // 提升属性
        void Increase(int value);

        // 提升属性的增幅
        void IncreaseAmplify(float value);

        // 更新数值
        void Update();
    }

    public interface ICharacterAttributeEx : ICharacterAttribute
    {
        // 原始数值
        int OriginValue { get; }

        // 消除属性加成
        void Reset();
    }
}
