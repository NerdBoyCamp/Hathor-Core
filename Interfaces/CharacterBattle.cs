using System;

namespace Hathor
{
    // 战斗相关
    public interface ICharacterBattle
    {
        // ---------------------------------------------------
        // 最大血量
        int MaxHP { get; }

        // 最大怒气
        int MaxAP { get; }

        // 力量
        int Strength { get; }

        // 智力
        int Intelligence { get; }

        // 敏捷
        int Dexterity { get; }

        // 获取额外属性值
        int GetAttrValue(string attr);

        // 降低属性
        void Demote(string attr, string id, int value);

        // 降低属性的增幅
        void DemoteAmplify(string attr, string id, float value);

        // 提升属性
        void Promote(string attr, string id, int value);

        // 提升属性的增幅
        void PromoteAmplify(string attr, string id, float value);

        // 消除所有提升和降低
        void PromoteClear(string id);

        // --------------------------------------------------
        // 血量
        int HP { get; }

        // 怒气
        int AP { get; }

        // 延时回复计算
        void HealDefer(string series, int value);

        // 延时增益回复计算
        void HealDeferAmplify(string series, float value);

        // 延时伤害计算
        void DamageDefer(string series, int value);

        // 延时增益伤害计算
        void DamageDeferAmplify(string series, float value);

        // 结算伤害/回复
        void DamageFlush();
    }
}
