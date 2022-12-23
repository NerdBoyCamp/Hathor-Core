namespace Hathor
{
    class HealEffectClass : IEffectClass
    {
        protected string mID;

        protected string mSeries;

        protected int mHealAmount; // 回复的HP

        protected float mHealSeconds; // 在多少时间内回复

        public HealEffectClass(
            string id,
            string series,
            int healAmount,
            float healSeconds
        ) {
            this.mID = id;
            this.mSeries = series;
            this.mHealAmount = healAmount;
            this.mHealSeconds = healSeconds;
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

        // 是否可影响建筑
        public bool IsAppliableOnBuilding { get => false; }

        // 是否可影响角色
        public bool IsAppliableOnCharacter { get => true; }

        // 是否可影响道具
        public bool IsAppliableOnItem { get => false; }

        // 通过建筑生成
        public IEffect CreateByBuilding(IBuilding building)
        {
            return new HealEffect(this, Util.RamdonID());
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            return new HealEffect(this, Util.RamdonID());
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return new HealEffect(this, Util.RamdonID());
        }


        class HealEffect : IEffect
        {
            protected HealEffectClass mCls;

            protected string mID;

            protected int mHealAmount; // 剩下要回复的HP

            protected float mHealLastTime; // 上一次回复的时间

            public HealEffect(HealEffectClass cls, string id)
            {
                this.mCls = cls;
                this.mID = id;
            }

            public string ID { get => this.mID; }

            // 描述
            public string Desctiption { get => ""; }

            // 是否效果结束
            public bool IsFinished { get => this.mHealAmount <= 0; }

            // 对应的效果类（角色能力/道具能力）
            public IEffectClass GetClass() { return this.mCls; }

            // 对建筑产生效果
            public void ApplyOnBuilding(IBuilding building) {}

            // 对角色产生效果
            public void ApplyOnCharacter(ICharacter character)
            {
                var battle = character.GetBattle();
                if (battle == null)
                {
                    this.mHealAmount = 0;
                }
                else
                {
                    // battle.DeferHeal(this.mCls.Series, this.mDefence);
                }
            }

            // 对物品产生效果
            public void ApplyOnItem(IItem item) {}
        }
    }
}
