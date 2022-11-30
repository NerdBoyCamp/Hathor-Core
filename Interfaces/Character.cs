namespace Hathor
{
    // 角色类（可作为NPC或者敌人的创建模板）
    public interface ICharacterClass
    {
        //
        string ID { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 创建角色
        ICharacter create();
    }

    public interface ICharacterGrowth
    {
        // 经验值
        int EXP { get; }

        // 等级
        int LV { get; }

        // 可用加点
        int AvailablePoint { get; }

        // 增加经验
        int increaseEXP(int exp);

        // 消耗点数
        int decreasePoint(int point);
    }

    // 角色能力/技能
    public interface ICharacterAbilities
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

    // 角色装备
    public interface ICharacterEquipments
    {
        // 装备物品
        IItem addEquipment(IItem item);

        // 卸下物品
        IItem removeEquipment(string itemID);

        // 获取所有装备的物品
        IItem[] listEquipments();
    }
    
    // 角色包裹
    public interface ICharacterBag
    {
        // 名字
        string Name { get; }

        // 获取包裹容量 
        int Capacity { get; }

        // 增加/减少包裹容量
        bool extendCapacity(int slotCount);

        // 交换位置
        bool swapItem(int slot1, int slot2);

        // 保存物品
        IItem storeItem(int slot, IItem item);
        
        // 丢弃物品
        IItem dropItem(int slot);
        
        // 查看物品
        IItem getItem(int slot);
    }

    public interface ICharacter
    {
        string ID { get; }

        // 名字
        string Name { get; set; }

        // 对应的角色类
        ICharacterClass getClass();

        // 当前角色成长
        ICharacterGrowth getGrowth();

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        IEffect[] getEffects();

        // 当前角色能力/技能
        ICharacterAbilities getAbilities();

        // 当前角色装备
        ICharacterEquipments getEquipments(); 

        // 当前角色的背包
        ICharacterBag[] getBags();
    }

    // public interface ICharacterForAdventure
    // {
    //     // 感应
    //     int Perception { get; }

    //     // 运气
    //     int Luck { get; }

    //     // 口才
    //     int Eloquence { get; }

    //     // 相貌
    //     int Appearance { get; }
    // }

    // public interface ICharacterForBattle
    // {
    //     int HP { get; }

    //     // 怒气
    //     int AP { get; }

    //     // 力量
    //     int Strength { get; }

    //     // 智力
    //     int Intelligence { get; }

    //     // 敏捷
    //     int Agile { get; }
    // }
}

