namespace Hathor
{
    public interface ICharacterCreater
    {
        ICharacter Create(string clsID);
        
        ICharacterClass[] ListClass();

        ICharacterClass FindClass(string clsID);
    }
}