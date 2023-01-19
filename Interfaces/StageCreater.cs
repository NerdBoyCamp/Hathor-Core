namespace Hathor
{
    public interface IStageCreater
    {
        // 创建场景
        IStage Create(
            string templateId,
            IBuildingCreater buildingCreater,
            ICharacterCreater characterCreater,
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater,
            IActionCreater actionCreater
        );
    }
}