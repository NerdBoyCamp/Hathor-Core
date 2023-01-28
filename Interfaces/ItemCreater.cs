namespace Hathor
{
    public interface IItemCreater
    {
        IItem Create(
            string templateId,
            IAbilityCreater abilityCreater
        );
    }
}