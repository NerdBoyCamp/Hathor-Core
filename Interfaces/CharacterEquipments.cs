namespace Hathor
{
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

        // 获取装备位上的物品
        IItem GetItem(string slot);

        // 装备物品
        IItem EquipItem(string slot, IItem item);

        // // 装备饰品
        // IItem EquipOrnament(IItem item);

        // // 卸下饰品
        // IItem RemoveOrnament(string itemID);

        // // 获取所有装饰品
        // IItem[] ListOrnaments();
    }
}
