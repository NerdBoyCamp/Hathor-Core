using System;

namespace Hathor
{
    class DamageEffectClass : IEffectClass
    {
        protected string mID;

        protected string mName;

        protected string mSeries;

        protected float mMaxDamage;

        protected float mMinDamage;

        public DamageEffectClass(
            string id,
            string name,
            string series,
            float maxDamage,
            float minDamage
        )
        {
            this.mID = id;
            this.mName = name;
            this.mSeries = series;
            this.mMaxDamage = Math.Max(maxDamage, minDamage);
            this.mMinDamage = Math.Min(maxDamage, minDamage);
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => this.mSeries; }

        // 名字
        public string Name { get => this.mName; }

        // 描述
        public string Desctiption { get => ""; }

        // 优先级
        public int Priority { get => 100; }

        // 自动施放
        public bool IsAuto { get => false; }

        // 对自己施放
        public bool IsSelf { get => false; }

        // 不会重复影响（效果不会叠加）
        public bool IsExclusive { get => false; }

        // 是否可影响建筑
        public bool IsAppliableOnBuilding { get => false; }

        // 是否可影响角色
        public bool IsAppliableOnCharacter { get => true; }

        // 是否可影响道具
        public bool IsAppliableOnItem { get => false; }

        // 通过建筑生成
        public IEffect CreateByBuilding(IBuilding building)
        {
            return null;
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            ICharacterBattle battle = character.GetBattle();
            if (battle == null)
            {
                return null;
            }

            return new DamageEffect(
                this,
                Util.RandomID(),
                (float)Util.RandomDouble(this.mMinDamage, this.mMaxDamage));
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            if (item.IsUsedUp)
            {
                // 使用完的的道具
                return null;
            }

            IItemBattle itemBattle = item.GetBattle();
            if (itemBattle == null)
            {
                return null;
            }

            ICharacter itemUser = itemBattle.User;
            if (itemUser == null)
            {
                return null;
            }

            ICharacterBattle userBattle = itemUser.GetBattle();
            if (userBattle == null)
            {
                return null;
            }

            // 计算发挥度
            float perf = itemBattle.UserPerformance;
            if (perf <= 0)
            {
                return null;
            }


            float damage = (float)Util.RandomDouble(
                this.mMinDamage, this.mMaxDamage) * perf;

            IAttribute userAttr = userBattle.GetAttribute(this.mSeries);
            if (userAttr != null)
            {
                // 加上人物基础伤害，如果有的话
                damage += userAttr.Value;
            }

            return new DamageEffect(this, Util.RandomID(), damage);
        }

        class DamageEffect : IEffect
        {
            protected DamageEffectClass mCls;

            protected string mID;

            protected float mDamage;

            protected bool mIsFinished = false;

            public DamageEffect(DamageEffectClass cls, string id, float damage)
            {
                this.mCls = cls;
                this.mID = id;
                this.mDamage = damage;
            }

            public string ID { get => this.mID; }

            // 描述
            public string Desctiption { get => null; }

            // 是否效果结束
            public bool IsFinished { get => this.mIsFinished; }

            // 对应的效果类（角色能力/道具能力）
            public IEffectClass GetClass() { return this.mCls; }

            // 对建筑产生效果
            public void ApplyOnBuilding(IBuilding building) { }

            // 对角色产生效果
            public void ApplyOnCharacter(ICharacter character)
            {
                ICharacterBattle battle = character.GetBattle();
                if (battle != null)
                {
                    battle.HP.GetDamageBuffer(
                        this.mCls.Series).Increase(this.mDamage);
                }
                this.mIsFinished = true;
            }

            // 对物品产生效果
            public void ApplyOnItem(IItem item) { }
        }

    }
}
