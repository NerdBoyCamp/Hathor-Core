namespace Hathor
{
    public interface ICharacterUpgrade
    {
        // 经验值
        int EXP { get; }

        // 等级
        int LV { get; }

        // 总点数
        int TotalPoints { get; }

        // 已用加点
        int UsedPoints { get; }

        // 增加/消耗经验
        bool IncreaseEXP(int exp);

        // 增加/消耗总点数
        bool IncreaseTotalPoints(int point);

        // 增加/消耗总点数
        bool IncreaseUsedPoints(int point);
    }
}
