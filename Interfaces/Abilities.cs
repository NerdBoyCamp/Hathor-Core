namespace Hathor
{
    public interface IAbilities
    {
        // 添加能力
        IEffectClass AddAbility(IEffectClass effectClass);

        // 删除能力
        IEffectClass RemoveAbility(string effectClassID);

        // 查找能力
        IEffectClass GetAbility(string effectClassID);

        // 获取所有能力
        IEffectClass[] ListAbilities();
    }
}
