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
        ICharacter Create();
    }

    public interface ICharacterAttributes
    {
        // 感应
        int Perception { get; }

        // 运气
        int Luck { get; }

        // 口才
        int Eloquence { get; }

        // 相貌
        int Appearance { get; }

        // 力量
        int Strength { get; }

        // 智力
        int Intelligence { get; }

        // 敏捷
        int Agile { get; }
    }

    // 战斗相关
    public interface ICharacterBattle
    {
        // 血量
        int HP { get; }

        // 最大血量
        int MaxHP { get; set; }

        // 怒气
        int AP { get; }

        // 最大怒气
        int MaxAP { get; set; }

        // 延时回复计算
        void DeferHeal(string series, int value);

        void DeferHealUp(string series, float value);

        // 延时伤害计算
        void DeferDamage(string series, int value);

        void DeferDamageUp(string series, float value);

        // 增加/消耗AP
        bool IncreaseAP(int value);

        bool DecreaseAP(int value);

        // 更新角色受到伤害
        void Update();
    }

    public interface ICharacterGrowth
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

    // 角色受到影响的效果（技能效果/抗性/buff/debuff）
    public interface ICharacterEffects
    {
        // 添加效果
        IEffect AddEffect(IEffect effect);

        // 删除效果
        IEffect RemoveEffect(string effectID);

        // 获取所有效果(按照优先级降序排列)
        IEffect[] ListEffects();

        // 更新角色身上效果
        void Update();
    }

    // 角色能力/技能
    public interface ICharacterAbilities
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

    // 角色装备
    public interface ICharacterEquipments
    {
        // 头
        IItem Head { get; }

        // 右手
        IItem RightHand { get; }

        // 左手
        IItem LeftHand { get; }

        // 胸
        IItem Chest { get; }

        // 腰
        IItem Waist { get; }

        // 脚
        IItem Feet { get; }

        // 装备物品，返回之前的物品
        IItem EquipHead(IItem item);

        // 装备，返回之前的装备
        IItem EquipRightHand(IItem item);

        // 装备，返回之前的装备
        IItem EquipLeftHand(IItem item);

        // 装备，返回之前的装备
        IItem EquipChest(IItem item);

        // 装备，返回之前的装备
        IItem EquipWaist(IItem item);

        // 装备，返回之前的装备
        IItem EquipFeet(IItem item);

        // // 装备饰品
        // IItem EquipOrnament(IItem item);

        // // 卸下饰品
        // IItem RemoveOrnament(string itemID);

        // // 获取所有装饰品
        // IItem[] ListOrnaments();
    }

    // 角色包裹
    public interface ICharacterStore
    {
        // 名字
        string Name { get; }

        // 获取包裹容量
        int Capacity { get; }

        // // 是否锁定
        // bool isLocked { get; set; }
        // 增加/减少包裹容量
        bool ExtendCapacity(int slotCount);

        // 交换位置
        bool SwapItem(int slot1, int slot2);

        // 保存物品
        IItem StoreItem(int slot, IItem item);

        // 丢弃物品
        IItem DropItem(int slot);

        // 查看物品
        IItem GetItem(int slot);
    }

    public interface ICharacterStores
    {
        // 随身背包
        ICharacterStore Knapsack { get; }

        // 仓库
        ICharacterStore Warehouse { get; }
    }

    public interface ICharacter
    {
        string ID { get; }

        // 名字
        string Name { get; set; }

        // 对应的角色类
        ICharacterClass GetClass();

        // 当前角色属性
        ICharacterAttributes GetAttributes();

        // 当前角色战斗相关
        ICharacterBattle GetBattle();

        // 当前角色成长
        ICharacterGrowth GetGrowth();

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        ICharacterEffects GetEffects();

        // 当前角色能力/技能
        ICharacterAbilities GetAbilities();

        // 当前角色装备
        ICharacterEquipments GetEquipments();

        // 当前角色的背包
        ICharacterStores GetStores();

        // 触发角色变化的事件
        void Publish(IEvent ev);

        // 订阅角色变化的事件/设置监听者
        void Subscribe(IEventListener listener);

        // 更新角色/每帧调用
        void Update();
    }
}
