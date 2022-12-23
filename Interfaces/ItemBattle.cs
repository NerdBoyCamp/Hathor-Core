namespace Hathor
{
    // 物品装备推荐条件（决定发挥度）
    public interface IItemBattle
    {
        // 力量增益
        int StrengthEnhancement { get; }

        // 力量需求
        int StrengthReqired { get; }

        // 智力增益
        int IntelligenceEnhancement { get; }

        // 智力需求
        int IntelligenceReqired { get; }

        // 敏捷增益
        int DexterityEnhancement { get; }

        // 敏捷需求
        int DexterityReqired { get; }
    }
}
