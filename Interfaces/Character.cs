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

        // 创建
        ICharacter Create(object configs);
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
        IAbilities GetAbilities();

        // 触发角色变化的事件
        void Publish(IEvent ev);

        // 订阅角色变化的事件/设置监听者
        void Subscribe(IEventListener listener);

        // 更新角色/每帧调用
        void Update();
    }
}
