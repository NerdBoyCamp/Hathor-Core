namespace Hathor
{
    public interface IEffects
    {
        // 添加效果
        IEffect AddEffect(IEffect effect);

        // 删除效果
        IEffect RemoveEffect(string effectID);

        // 获取所有效果(按照优先级降序排列)
        IEffect[] ListEffects();

        // 更新效果
        void Update();
    }

    public interface IEffectAbilities
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
