namespace Hathor
{
    public interface IStage
    {
        // 查找建筑
        IBuilding FindBuilding(string buildingID);

        // 查找角色
        ICharacter FindCharacter(string characterID);

        // 查找物品
        IItem FindItem(string itemID);

        // 场景更新
        void Update(float deltaTime);
    }
}
