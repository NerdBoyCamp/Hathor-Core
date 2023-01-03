namespace Hathor
{
    public interface IStage
    {
        // 添加建筑
        bool AddBuilding(IBuilding building);

        // 添加角色
        bool AddCharacter(ICharacter character);

        // 添加物品
        bool AddItem(IItem item);

        // 查找建筑
        IBuilding FindBuilding(string buildingID);

        // 查找角色
        ICharacter FindCharacter(string characterID);

        // 查找物品
        IItem FindItem(string itemID);

        // 场景更新
        void Update();
    }
}
