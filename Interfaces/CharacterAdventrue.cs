namespace Hathor
{
    // 冒险相关
    public interface ICharacterAdventure
    {
        // 感应
        ICharacterAttribute Perception { get; }

        // 运气
        ICharacterAttribute Luck { get; }

        // 口才
        ICharacterAttribute Eloquence { get; }

        // 相貌
        ICharacterAttribute Appearance { get; }

        // 获取额外属性值
        ICharacterAttribute GetAttribute(string attrName);
    }
}
