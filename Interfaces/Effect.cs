namespace Hathor
{
    public interface IEffectClass
    {
        string ID { get; }

        // 系列（物理/电/辐射/迷信/其他...）
        string Series { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 优先级
        int Priority { get; }

        // 自动施放
        bool IsAuto { get; }

        // 对自己施放
        bool IsSelf { get; }

        // 是否可影响建筑
        bool IsAppliableOnBuilding { get; }

        // 是否可影响角色
        bool IsAppliableOnCharacter { get; }

        // 是否可影响道具
        bool IsAppliableOnItem { get; }

        // 通过建筑生成
        IEffect CreateByBuilding(IBuilding building);

        // 通过角色生成
        IEffect CreateByCharacter(ICharacter character);

        // 通过物品生成
        IEffect CreateByItem(IItem item);
    }

    public interface IEffect
    {
        string ID { get; }

        // 描述
        string Desctiption { get; }

        // 是否效果结束
        bool IsFinished { get; }

        // 对应的效果类（角色能力/道具能力）
        IEffectClass GetClass();

        // 对建筑产生效果
        void ApplyOnBuilding(IBuilding building);

        // 对角色产生效果
        void ApplyOnCharacter(ICharacter character);

        // 对物品产生效果
        void ApplyOnItem(IItem item);
    }
}
