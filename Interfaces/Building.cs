namespace Hathor
{
    public interface IBuildingClass
    {
        string ID { get; }

        // 名字
        string Name { get; }

        // 创建建筑
        IBuilding create();
    }

    public interface IBuildingGrowth
    {
        // 等级
        int LV { get; }
    }

    public interface IBuilding
    {
        string ID { get; }

        // 名字
        string Name { get; set; }

        // 描述
        string Desctiption { get; set; }

        // 对应建筑类
        IBuildingClass getClass();

        // 当前建筑成长
        IBuildingGrowth getGrowth();
    }
}
