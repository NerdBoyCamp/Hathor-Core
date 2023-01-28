namespace Hathor
{
    public interface IBuildingCreater
    {
        IBuilding Create(
            string templateId,
            IAbilityCreater abilityCreater
        );
    }
}
