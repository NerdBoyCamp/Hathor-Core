namespace Hathor
{
    // 战斗相关
    public interface ICharacterBattle
    {
        // 血量
        int HP { get; }

        // 最大血量
        int MaxHP { get; }

        // 怒气
        int AP { get; }

        // 最大怒气
        int MaxAP { get; }

        // 力量
        int Strength { get; }

        // 智力
        int Intelligence { get; }

        // 敏捷
        int Dexterity { get; }

        // 延时回复计算
        void DeferHeal(string series, int value);

        // 延时增益回复计算
        void DeferHealUp(string series, float value);

        // 延时伤害计算
        void DeferDamage(string series, int value);

        // 延时增益伤害计算
        void DeferDamageUp(string series, float value);

        // 结算伤害
        void FlushDamage();
    }
}
