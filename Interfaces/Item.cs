namespace Hathor
{
    public interface IItemClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 创建
        IItem create();
    }

    // 物品能力/技能
    public interface IItemAbilities
    {
        // 添加能力
        IEffectClass addAbility(IEffectClass effectClass);

        // 删除能力
        IEffectClass removeAbility(string effectClassID);

        // 查找能力
        IEffectClass getAbility(string effectClassID);
        
        // 获取所有能力(按照优先级降序排列)
        IEffectClass[] listAbilities();
    }

    public interface IItem
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 是否可用
        bool isUsable { get; }

        // 道具类
        IItemClass getClass();

        // 当前道具受到影响的效果（技能效果/抗性/buff/debuff）
        IEffect[] getEffects();

        // 当前道具拥有的可触发效果（能力/技能）
        IItemAbilities getAbilities();

       // 判断建筑是否可以装备物品
        bool isEquipableByBuilding(IBuilding building);

        // 判断角色是否可以装备物品
        bool isEquipableByCharacter(ICharacter character);
    }

    // public interface IItemQualification
    // {
    //     // 需要力量
    //     int StrengthRequired { get; }

    //     // 需要智力
    //     int IntelligenceRequired { get; }

    //     // 需要敏捷
    //     int AgileRequired { get; }
    // }

    // public interface IItemForBattle
    // {
    //     // 最小伤害
    //     int MinDamage { get; }

    //     // 最大伤害
    //     int MaxDamage { get; }
    // }

    // public interface IItemBattleEx
    // {
    //     // 电系伤害
    //     int MaxElectricityDamage { get; }

    //     int MinElectricityDamage { get; }

    //     // 毒系伤害
    //     int MaxPoisonDamage { get; }

    //     int MaxPoisonDamage { get; }

    //     // 迷信伤害
    //     int MaxSupersitionDamage { get; }

    //     int MinSupersitionDamage { get; }

    //     // 辐射伤害
    //     int MaxRadiationDamage { get; }

    //     int MinRadiationDamage { get; }
    // }
}
