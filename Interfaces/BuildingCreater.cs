namespace Hathor
{
    public interface IBuildingCreater
    {
        IBuilding Create(string clsID);

        IBuildingClass[] ListClass();

        IBuildingClass FindClass(string clsID);
    }
}
