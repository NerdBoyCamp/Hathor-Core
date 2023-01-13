namespace Hathor
{
    public interface IActionCreater
    {
        string ID { get; }

        string Name { get; }

        IAction[] CreateByBuilding(IBuilding building);

        IAction[] CreateByCharacter(ICharacter character);

        IAction[] CreateByItem(IItem item);
    }
}