namespace Hathor
{
    public interface IItemClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 类别 头盔/武器/胸甲/鞋子/腰带/回复道具/...
        string Series { get; }

        // 创建
        IItem Create();
    }

    public interface IItem
    {
        // ID
        string ID { get; }

        // 名字
        string Name { get; }

        // 是否可用
        bool IsUsable { get; }

        // 是否已用完（销毁物品）
        bool IsUsedUp { get; }

        // 当前使用者
        ICharacter User { get; set; }

        // 道具类
        IItemClass GetClass();

        // 当前道具战斗相关
        IItemBattle GetBattle();

        // 当前道具受到影响的效果（技能效果/抗性/buff/debuff）
        IEffects GetEffects();

        // 当前道具拥有的可触发效果（能力/技能）
        IEffectAbilities GetAbilities();

        // 触发物品变化的事件
        void Publish(IEvent ev);

        // 订阅物品变化的事件/设置监听者
        void Subscribe(IEventListener listener);

        // 更新物品/每帧调用
        void Update();
    }
}
