using System.Collections.Generic;

namespace Hathor
{
    class PromoteEffectClass : IEffectClass
    {
        protected string mID;

        protected string mName;

        protected Dictionary<string, float> mAdvAttrsIncreases;

        protected Dictionary<string, float> mAdvAttrsExpands;

        protected Dictionary<string, float> mBttlAttrsIncreases;

        protected Dictionary<string, float> mBttlAttrsExpands;

        public PromoteEffectClass(
            string id,
            string name,
            Dictionary<string, float> adventureIncreases,
            Dictionary<string, float> adventureExpands,
            Dictionary<string, float> battleIncreases,
            Dictionary<string, float> battleExpands
        )
        {
            this.mID = id;
            this.mName = name;
            this.mAdvAttrsIncreases = new Dictionary<string, float>(adventureIncreases);
            this.mAdvAttrsExpands = new Dictionary<string, float>(adventureExpands);
            this.mBttlAttrsIncreases = new Dictionary<string, float>(battleIncreases);
            this.mBttlAttrsExpands = new Dictionary<string, float>(battleExpands);
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => "Buf"; }

        // 名字
        public string Name { get => this.mName; }

        // 描述
        public string Desctiption { get => ""; }

        // 优先级
        public int Priority { get => 100; }

        // 自动施放
        public bool IsAuto { get => true; }

        // 对自己施放
        public bool IsSelf { get => true; }

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
            return new PromoteEffect(this, Util.RandomID());
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            return new PromoteEffect(this, Util.RandomID());
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return new PromoteEffect(this, Util.RandomID());
        }


        class PromoteEffect : IEffect
        {
            protected PromoteEffectClass mCls;
            protected string mID;
            protected Dictionary<string, IAttributeChange> mAttrsChanges;

            public PromoteEffect(PromoteEffectClass cls, string id)
            {
                this.mCls = cls;
                this.mID = id;
                this.mAttrsChanges = new Dictionary<string, IAttributeChange>();
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
                ICharacterAdventure adventure = character.GetAdventure();
                if (adventure != null)
                {
                    // 按数值增益
                    foreach (var attr in this.mCls.mAdvAttrsIncreases)
                    {
                        if (attr.Value == 0)
                        {// 增益为0，直接跳过
                            continue;
                        }

                        IAttributeChange attrChg = adventure.GetAttribute(
                            attr.Key).Increase(attr.Value);
                        this.mAttrsChanges.Add(attr.Key, attrChg);
                    }

                    // 按比例增益
                    foreach (var attr in this.mCls.mAdvAttrsExpands)
                    {
                        if (attr.Value == 0)
                        {// 增益为0%，直接跳过
                            continue;
                        }

                        IAttributeChange attrChg = adventure.GetAttribute(
                            attr.Key).Expand(attr.Value + 1.0f);
                        this.mAttrsChanges.Add(attr.Key, attrChg);
                    }
                }

                ICharacterBattle battle = character.GetBattle();
                if (battle != null)
                {
                    // 按数值增益
                    foreach (var attr in this.mCls.mBttlAttrsIncreases)
                    {
                        if (attr.Value == 0)
                        {// 增益为0，直接跳过
                            continue;
                        }

                        IAttributeChange attrChg = battle.GetAttribute(
                            attr.Key).Increase(attr.Value);
                        this.mAttrsChanges.Add(attr.Key, attrChg);
                    }

                    // 按比例增益
                    foreach (var attr in this.mCls.mBttlAttrsExpands)
                    {
                        if (attr.Value == 0)
                        {// 增益为0，直接跳过
                            continue;
                        }

                        IAttributeChange attrChg = battle.GetAttribute(
                            attr.Key).Expand(attr.Value + 1.0f);
                        this.mAttrsChanges.Add(attr.Key, attrChg);
                    }
                }
            }

            // 对物品产生效果
            public void ApplyOnItem(IItem item) { }
        }
    }
}
