namespace Hathor
{
    // 物品装备推荐条件（决定发挥度）
    public interface IItemBattle
    {
        // 当前使用者
        ICharacter User { get; }

        // 力量需求
        int StrengthRequired { get; }

        // 力量提升
        int StrengthPromoted { get; }

        // 智力需求
        int IntelligenceRequired { get; }

        // 智力提升
        int IntelligencePromoted { get; }

        // 敏捷需求
        int DexterityRequired { get; }

        // 敏捷提升
        int DexterityPromoted { get; }

        // 获取额外属性需求值
        int GetAttrValueRequired(string attr);

        // 获取额外属性提升值
        int GetAttrValuePromoted(string attr);

        // 当前使用角色的发挥度
        float GetUserPerformance();

        // 设置当前使用者
        bool SetUser(ICharacter character);
    }
}
