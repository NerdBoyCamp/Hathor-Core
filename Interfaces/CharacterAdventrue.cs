namespace Hathor
{
    // 冒险相关
    public interface ICharacterAdventure
    {
        // 感应
        IAttribute Perception { get; }

        // 运气
        IAttribute Luck { get; }

        // 口才
        IAttribute Eloquence { get; }

        // 相貌
        IAttribute Appearance { get; }

        // 获取额外属性值
        IAttribute GetAttribute(string attrName);
    }
}
