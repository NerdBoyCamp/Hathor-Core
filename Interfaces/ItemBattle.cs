namespace Hathor
{
    // 物品装备推荐条件（决定发挥度）
    public interface IItemBattleAttributes
    {
        // 力量需求
        IAttribute Strength { get; }

        // 智力需求
        IAttribute Intelligence { get; }

        // 敏捷需求
        IAttribute Dexterity { get; }

        // 获取额外属性需求值
        IAttribute GetAttribute(string attrName);

        // 列出所有属性名
        string[] ListAttributeName();
    }

    public interface IItemBattle
    {
        // 属性需求
        IItemBattleAttributes Requirments { get; }

        // 属性的增益/减益
        IItemBattleAttributes Enhancements { get; }

        // 当前使用者
        ICharacter User { get; set; }

        // 当前使用角色的发挥度
        float UserPerformance { get; }
    }
}
