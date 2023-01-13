namespace Hathor
{
    public interface IItemCreater
    {
        IItem Create(string clsID);

        IItemClass[] ListClass();

        IItemClass FindClass(string clsID);
    }
}