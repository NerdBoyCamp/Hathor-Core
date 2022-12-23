namespace Hathor
{
    public interface IItemClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 标签
        string[] Tags { get; }

        // 创建
        IItem Create();
    }

    // 物品受到影响的效果（技能效果/抗性/buff/debuff）
    public interface IItemEffects
    {
        // 添加效果
        IEffect AddEffect(IEffect effect);

        // 删除效果
        IEffect RemoveEffect(string effectID);

        // 获取所有效果
        IEffect[] ListEffects();

        // 更新状态
        void Update();
    }

    // 物品能力/技能
    public interface IItemAbilities
    {
        // 添加能力
        IEffectClass AddAbility(IEffectClass effectClass);

        // 删除能力
        IEffectClass RemoveAbility(string effectClassID);

        // 查找能力
        IEffectClass GetAbility(string effectClassID);

        // 获取所有能力
        IEffectClass[] ListAbilities();
    }

    public interface IItem
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 是否可用
        bool IsUsable { get; }

        // 道具类
        IItemClass GetClass();

        // 当前道具受到影响的效果（技能效果/抗性/buff/debuff）
        IItemEffects GetEffects();

        // 当前道具拥有的可触发效果（能力/技能）
        IItemAbilities GetAbilities();

        // 当前道具战斗相关
        IItemBattle GetBattle();

        // 判断建筑是否可以装备物品
        bool IsEquipableByBuilding(IBuilding building);

        // 判断角色是否可以装备物品
        bool IsEquipableByCharacter(ICharacter character);
    }

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
