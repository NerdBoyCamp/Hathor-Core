namespace Hathor
{
    // 角色装备
    public interface ICharacterEquipments
    {
        // 头
        IItem Head { get; }

        // 主手武器
        IItem Main { get; }

        // 胸
        IItem Chest { get; }

        // 腰
        IItem Waist { get; }

        // 脚
        IItem Feet { get; }

        // 获取装备位上的物品
        IItem GetItem(string slot);

        // 装备物品
        IItem EquipItem(string slot, IItem item);

        // 卸下物品
        IItem RemoveItem(string slot);

        // 返回装备列表
        IItem[] ListItem();

        // 更新所有装备状态/每帧调用
        void Update();
    }
}
