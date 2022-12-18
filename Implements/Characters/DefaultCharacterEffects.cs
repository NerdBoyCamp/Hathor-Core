using System.Collections.Generic;

namespace Hathor
{
    // class DefaultCharacterEffectClass : IEffectClass {
    //     public string ID { get => null; }
    //     // 名字
    //     public string Name { get => "Default Attributes"; }
    //     // 描述
    //     public string Desctiption { get => "Strength, Intelligence, Agile"; }
    //     // 优先级
    //     public int Priority { get => 0; }
    //     // 自动施放
    //     public bool IsAuto { get => false; }
    //     // 对自己施放 ?
    //     public bool IsSelf { get => false; }
    //     // 对应的类的组
    //     public IEffectClassGroup GetClassGroup() {
    //         return null;
    //     }
    //     // 通过建筑生成
    //     public IEffect CreateByBuilding(IBuilding building) {
    //         return null;
    //     }
    //     // 通过角色生成
    //     public IEffect CreateByCharacter(ICharacter character) {
    //         return null;
    //     }
    //     // 通过物品生成
    //     public IEffect CreateByItem(IItem item) {
    //         return null;
    //     }
    // }
    // class DefaultCharacterEffect : IEffect {
    //     // 类
    //     protected IEffectClass mCls = null;
    //     // ID
    //     protected string mID = null;
    //     // ID
    //     public string ID { get => this.mID; }
    //     // 描述
    //     public string Desctiption { get => "Hit"; }
    //     // 持续产生效果
    //     public bool IsFinished { get => true; }
    //     // 对应的效果类（角色能力/道具能力）
    //     public IEffectClass GetClass() { return this.mCls; }
    //     // 对建筑产生效果 (返回false, 表示无效果)
    //     public void ApplyOnBuilding(IBuilding building) { }
    //     // 对角色产生效果 (返回false, 表示无效果)
    //     public void ApplyOnCharacter(ICharacter character) { }
    //     // 对物品产生效果 (返回false, 表示无效果）
    //     public void ApplyOnItem(IItem item) { }
    // }
    class DefaultCharacterEffects : ICharacterEffects
    {
        // 角色
        protected ICharacter mChar;

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
        public DefaultCharacterEffects(ICharacter character)
        {
            this.mChar = character;
            this.mEffects = new List<IEffect> { };
            this.mIsSorted = true;
        }

        // 添加效果
        public IEffect AddEffect(IEffect effect)
        {
            if (!effect.IsAppliableOnCharacter)
            {
                // 无法对角色产生影响
                return null;
            }

            int index =
                this
                    .mEffects
                    .FindIndex((IEffect i) =>
                    {
                        return i.ID == effect.ID;
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
        public IEffect RemoveEffect(string effectID)
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

        public void Update()
        {
            this.SortEffects();
            for (int i = this.mEffects.Count - 1; i >= 0; i--)
            {
                // 逆向遍历，优先级高的先执行
                IEffect eff = this.mEffects[i];
                if (eff.IsFinished || !eff.IsAppliableOnCharacter)
                {
                    // 删除已经执行完的效果
                    this.mEffects.RemoveAt(i);
                }
                else
                {
                    eff.ApplyOnCharacter(this.mChar);
                    this.mChar.Publish(new EffectEvent { Effect = eff });
                }
            }
        }
    }
}
