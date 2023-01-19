namespace Hathor
{
    public interface ICharacterCreater
    {
        ICharacter Create(
            string templateId,
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater
        );
    }
}