namespace Hathor
{
    public interface IBuildingClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 创建实例
        IBuilding Create(dynamic configs);
    }

    public interface IBuilding
    {
        string ID { get; }

        // 名字
        string Name { get; set; }

        // 描述
        string Desctiption { get; set; }

        // 对应建筑类
        IBuildingClass GetClass();

        // 当前建筑成长
        IBuildingUpgrade GetUpgrade();

        // 当前建筑更新
        void Update(float deltaTime);
    }
}
