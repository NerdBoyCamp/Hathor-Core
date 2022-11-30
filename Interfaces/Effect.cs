namespace Hathor
{
    public interface IEffectClassGroup
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }
    }

    public interface IEffectClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 优先级
        int Priority { get; }

        // 自动施放
        bool isAuto { get; }

        // 对自己施放 ?
        bool isSelf { get; }

        // 对应的类的组
        IEffectClassGroup getClassGroup();

        // 通过建筑生成
        IEffect createByBuilding(IBuilding building);

        // 通过角色生成
        IEffect createByCharacter(ICharacter character);

        // 通过物品生成
        IEffect createByItem(IItem item);
    }

    public interface IEffect
    {
        // 对应的效果类（角色能力/道具能力）
        IEffectClass getClass();

        // 持续产生效果时，判断是否效果结束（例如中毒效果）
        bool isFinished();

        // 对建筑产生效果 (返回false, 表示无效果)
        bool takeOnBuilding(IBuilding building);

        // 对角色产生效果 (返回false, 表示无效果)
        bool takeOnCharacter(ICharacter character);

        // 对物品产生效果 (返回false, 表示无效果）
        bool takeOnItem(IItem item);
    }
}
