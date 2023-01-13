namespace Hathor
{
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
}