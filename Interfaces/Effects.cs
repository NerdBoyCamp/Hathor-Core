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
}
