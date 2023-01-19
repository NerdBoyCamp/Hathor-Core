namespace Hathor
{
    public interface IActionCreater
    {
        IAction[] CreateByBuilding(IBuilding building);

        IAction[] CreateByCharacter(ICharacter character);

        IAction[] CreateByItem(IItem item);
    }
}