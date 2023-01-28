namespace Hathor
{
    class DefenceEffectClass : IEffectClass
    {
        protected string mID;

        protected string mSeries;

        protected float mDefence;

        public DefenceEffectClass(
            string id,
            string series,
            float defence
        )
        {
            this.mID = id;
            this.mSeries = series;
            this.mDefence = defence;
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => this.mSeries; }

        // 名字
        public string Name { get => ""; }

        // 描述
        public string Desctiption { get => ""; }

        // 优先级
        public int Priority { get => 0; }

        // 自动施放
        public bool IsAuto { get => true; }

        // 对自己施放
        public bool IsSelf { get => true; }

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

            return new DefenceEffect(this, Util.RandomID());
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return null;
        }

        class DefenceEffect : IEffect
        {
            protected DefenceEffectClass mCls;

            protected string mID;

            public DefenceEffect(DefenceEffectClass cls, string id)
            {
                this.mCls = cls;
                this.mID = id;
            }

            public string ID { get => this.mID; }

            // 描述
            public string Desctiption { get => ""; }

            // 是否效果结束
            public bool IsFinished { get => false; }

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
                        this.mCls.Series).Increase(-this.mCls.mDefence);
                }
            }

            // 对物品产生效果
            public void ApplyOnItem(IItem item) { }
        }
    }
}
