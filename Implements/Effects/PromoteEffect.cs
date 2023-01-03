using System.Collections.Generic;

namespace Hathor
{
    class PromoteEffectClass : IEffectClass
    {
        protected string mID;

        protected Dictionary<string, int> mAttributes;

        public PromoteEffectClass(
            string id,
            Dictionary<string, int> attrs
        )
        {
            this.mID = id;
            this.mAttributes = new Dictionary<string, int>(attrs);
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => ""; }

        // 名字
        public string Name { get => ""; }

        // 描述
        public string Desctiption { get => ""; }

        // 优先级
        public int Priority { get => 100; }

        // 自动施放
        public bool IsAuto { get => false; }

        // 对自己施放
        public bool IsSelf { get => false; }

        // 不会重复影响（效果不会叠加）
        public bool IsExclusive { get => true; }

        // 是否可影响建筑
        public bool IsAppliableOnBuilding { get => false; }

        // 是否可影响角色
        public bool IsAppliableOnCharacter { get => true; }

        // 是否可影响道具
        public bool IsAppliableOnItem { get => false; }

        // 通过建筑生成
        public IEffect CreateByBuilding(IBuilding building)
        {
            return new PromoteEffect(this, Util.RamdonID());
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            return new PromoteEffect(this, Util.RamdonID());
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return new PromoteEffect(this, Util.RamdonID());
        }


        class PromoteEffect : IEffect
        {
            protected PromoteEffectClass mCls;

            protected string mID;

            public PromoteEffect(PromoteEffectClass cls, string id)
            {
                this.mCls = cls;
                this.mID = id;
            }

            public string ID { get => this.mID; }

            // 描述
            public string Desctiption { get => ""; }

            // 是否效果结束
            public bool IsFinished { get => true; }

            // 对应的效果类（角色能力/道具能力）
            public IEffectClass GetClass() { return this.mCls; }

            // 对建筑产生效果
            public void ApplyOnBuilding(IBuilding building) { }

            // 对角色产生效果
            public void ApplyOnCharacter(ICharacter character)
            {
                var battle = character.GetBattle();
                if (battle == null)
                {
                    return;
                }

                foreach (var attr in this.mCls.mAttributes)
                {
                    battle.GetAttribute(attr.Key).
                    battle.Promote(attr.Key, this.ID, attr.Value);
                }
            }

            // 对物品产生效果
            public void ApplyOnItem(IItem item) { }
        }
    }
}
