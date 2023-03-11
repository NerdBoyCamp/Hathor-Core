namespace Hathor
{
    // 战斗相关
    public interface ICharacterBattle
    {
        // ---------------------------------------------------
        // 血量
        ICharacterHealth HP { get; }
        
        // 怒气
        ICharacterAnger AP { get; }

        // 最大血量
        IAttribute MaxHP { get; }

        // 最大怒气
        IAttribute MaxAP { get; }

        // 力量
        IAttribute Strength { get; }

        // 智力
        IAttribute Intelligence { get; }

        // 敏捷
        IAttribute Dexterity { get; }

        // 移动速度
        IAttribute Speed { get; }

        // 暴击率
        IAttribute CriticalHitRate { get; }

        // 暴击伤害
        IAttribute CriticalHitDamage { get; }

        // 基础物理伤害
        IAttribute PhysicalDamage { get; }

        // 基础魔法伤害
        IAttribute MagicalDamage { get; }

        // 获取额外属性值
        IAttribute GetAttribute(string attrName);

        // 更新属性
        void Update(float deltaTime);
    }
}
