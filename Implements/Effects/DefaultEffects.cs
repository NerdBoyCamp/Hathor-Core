using System.Collections.Generic;

namespace Hathor
{
    abstract class DefaultEffects : IEffects
    {
        // 效果列表
        protected List<IEffect> mEffects;

        // 当前是否已经排序
        protected bool mIsSorted;

        // 排序
        protected void SortEffects()
        {
            if (!this.mIsSorted)
            {
                this
                    .mEffects
                    .Sort((IEffect e1, IEffect e2) =>
                    {
                        int p1 = e1.GetClass().Priority;
                        int p2 = e2.GetClass().Priority;
                        if (p1 == p2)
                        {
                            return e1.ID.CompareTo(e2.ID);
                        }
                        return p1 < p2 ? -1 : 1;
                    });
                this.mIsSorted = true;
            }
        }

        // 构造
        public DefaultEffects()
        {
            this.mEffects = new List<IEffect> { };
            this.mIsSorted = true;
        }

        // 添加效果
        public virtual IEffect AddEffect(IEffect effect)
        {
            IEffectClass effectClass = effect.GetClass();
            int index =
                this
                    .mEffects
                    .FindIndex((IEffect i) =>
                    {
                        return i.ID == effect.ID ||
                        (
                        effectClass.IsExclusive &&
                        i.GetClass().ID == effectClass.ID
                        );
                    });
            if (index != -1)
            {
                // 已经加过了
                return null;
            }
            this.mEffects.Add(effect);
            this.mIsSorted = false;
            return effect;
        }

        // 删除效果
        public virtual IEffect RemoveEffect(string effectID)
        {
            int index =
                this
                    .mEffects
                    .FindIndex((IEffect i) =>
                    {
                        return i.ID == effectID;
                    });
            if (index == -1)
            {
                return null;
            }
            IEffect effect = this.mEffects[index];
            this.mEffects.RemoveAt(index);
            return effect;
        }

        // 获取所有效果(按照优先级降序排列)
        public IEffect[] ListEffects()
        {
            this.SortEffects();
            return this.mEffects.ToArray();
        }

        public void Update(float deltaTime)
        {
            this.SortEffects();
            for (int i = this.mEffects.Count - 1; i >= 0; i--)
            {
                // 逆向遍历，优先级高的先执行
                IEffect eff = this.mEffects[i];
                if (eff.IsFinished)
                {
                    // 删除已经执行完的效果
                    this.mEffects.RemoveAt(i);
                }
                else
                {
                    this.ApplyEffect(eff, deltaTime);
                }
            }
        }

        // 施加影响逻辑
        protected virtual void ApplyEffect(IEffect effect, float deltaTime)
        {
            // do nothing
        }
    }
}
