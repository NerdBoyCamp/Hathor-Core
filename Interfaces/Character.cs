using System;

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

        // 查找物品
        int FindItem(string itemID);
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

        // 当前角色冒险属性
        ICharacterAdventure GetAdventure();

        // 当前角色战斗相关
        ICharacterBattle GetBattle();

        // 当前角色成长
        ICharacterUpgrade GetUpgrade();

        // 当前角色装备
        ICharacterEquipments GetEquipments();

        // 当前角色的背包
        ICharacterStores GetStores();

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        IEffects GetEffects();

        // 当前角色能力/技能
        IEffectAbilities GetAbilities();

        // 触发角色变化的事件
        void Publish(IEvent ev);

        // 订阅角色变化的事件/设置监听者
        void Subscribe(IEventListener listener);

        // 更新角色/每帧调用
        void Update();
    }
}
