using System;

namespace Hathor
{
    public interface ICharacterBattleHP
    {
        // 血量值
        int Value { get; }

        // 伤害计算
        ICharacterAttribute GetDamageBuffer(string series);

        // 回复计算
        ICharacterAttribute GetHealingBuffer(string series);

        // 结算伤害/回复
        void Update();
    }
    
    // 战斗相关
    public interface ICharacterBattle
    {
        // ---------------------------------------------------
        // 血量
        int HP { get; }

        // 怒气
        int AP { get; }

        // 伤害计算
        ICharacterAttribute GetDamageBuffer(string series);

        // 回复计算
        ICharacterAttribute GetHealingBuffer(string series);

        // 最大血量
        ICharacterAttributeEx MaxHP { get; }

        // 最大怒气
        ICharacterAttributeEx MaxAP { get; }

        // 力量
        ICharacterAttributeEx Strength { get; }

        // 智力
        ICharacterAttributeEx Intelligence { get; }

        // 敏捷
        ICharacterAttributeEx Dexterity { get; }

        // 获取额外属性值
        ICharacterAttributeEx GetAttribute(string attrName);

        // 
        void Reset();

        // 更新属性
        void Update();
    }
}
