namespace Hathor
{
    public interface IAction
    {
        // 名字
        string Name { get; }

        // 描述
        string Desctiption { get; }

        // 是否可对建筑施放
        bool IsAppliableOnBuilding { get; }

        // 是否可对角色施放
        bool IsAppliableOnCharacter { get; }

        // 是否可对道具施放
        bool IsAppliableOnItem { get; }

        // 是否可直接释放
        bool IsAppliable { get; }

        // 对建筑施放
        void ApplyOnBuilding(IBuilding building);

        // 对角色施放
        void ApplyOnCharacter(ICharacter character);

        // 对物品施放
        void ApplyOnItem(IItem item);

        // 直接施放
        void Apply();
    }
}
