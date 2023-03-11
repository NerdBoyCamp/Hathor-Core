using System;

namespace Hathor
{
    class HealEffectClass : IEffectClass
    {
        protected string mID;

        protected string mSeries;

        protected float mHealAmount; // 回复的HP

        protected float mHealTime; // 在多少时间内回复

        protected float mHealAmountPerSec; // 平均每秒回复HP

        public HealEffectClass(
            string id,
            string series,
            float healAmount,
            float healTime
        )
        {
            this.mID = id;
            this.mSeries = series;
            this.mHealAmount = healAmount;
            this.mHealTime = healTime;
            this.mHealAmountPerSec =
                this.mHealTime == 0.0f ?
                this.mHealAmount :
                this.mHealAmount / this.mHealTime;
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => this.mSeries; }

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
            return new HealEffect(this, Util.RandomID());
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            return new HealEffect(this, Util.RandomID());
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return new HealEffect(this, Util.RandomID());
        }


        class HealEffect : IEffect
        {
            protected HealEffectClass mCls;

            protected string mID;

            protected float mHealAmount; // 已经回复的HP

            protected float mHealTime; // 已经回复的时间

            public HealEffect(HealEffectClass cls, string id)
            {
                this.mCls = cls;
                this.mID = id;
                this.mHealAmount = 0.0f;
                this.mHealTime = 0.0f;
            }

            public string ID { get => this.mID; }

            // 描述
            public string Desctiption { get => ""; }

            // 是否效果结束
            public bool IsFinished { get => this.mHealAmount >= this.mCls.mHealAmount; }

            // 对应的效果类（角色能力/道具能力）
            public IEffectClass GetClass() { return this.mCls; }

            // 对建筑产生效果
            public void UpdateOnBuilding(IBuilding building, float deltaTime) { }

            // 对角色产生效果
            public void UpdateOnCharacter(ICharacter character, float deltaTime)
            {
                if (this.mHealAmount >= this.mCls.mHealAmount)
                {
                    // no need to heal any more
                    return;
                }

                ICharacterBattle battle = character.GetBattle();
                if (battle == null)
                {
                    // make this effect finish
                    this.mHealAmount = this.mCls.mHealAmount;
                    return;
                }

                if (this.mCls.mHealTime == 0.0f)
                {
                    // make this effect finish
                    this.mHealAmount = this.mCls.mHealAmount;
                    // add all healamount to character
                    battle.HP.GetHealingBuffer(this.mCls.Series).Increase(this.mHealAmount);
                    return;
                }

                
                float deltaHealAmount = this.mCls.mHealAmountPerSec * deltaTime;
                float healAmount = this.mHealAmount + deltaHealAmount;
                
                if (healAmount > this.mCls.mHealAmount)
                {
                    // 调整回复血量，不超过最大值
                    deltaHealAmount = this.mCls.mHealAmount - this.mHealAmount;
                    this.mHealAmount = this.mCls.mHealAmount;
                }
                else
                {
                    this.mHealAmount = healAmount;
                }

                battle.HP.GetHealingBuffer(this.mCls.Series).Increase(deltaHealAmount);
            }

            // 对物品产生效果
            public void UpdateOnItem(IItem item, float deltaTime) { }
        }
    }
}
