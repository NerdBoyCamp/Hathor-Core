namespace Hathor
{
    public interface IStageCreater
    {
        // 创建场景
        IStage Create(
            IBuildingCreater buildingCreater,
            ICharacterCreater characterCreater,
            IItemCreater itemCreater,
            IActionCreater actionCreater
        );
    }
}