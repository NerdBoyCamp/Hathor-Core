namespace Hathor
{
    class HitEffectClass : IEffectClass
    {
        protected string mID;

        public HitEffectClass(string id) {
            this.mID = id;
        }

        public string ID { get => this.mID; }

        // 系列（物理/电/辐射/迷信/其他...）
        public string Series { get => "phys"; }

        // 名字
        public string Name { get => "Hit Effect"; }

        // 描述
        public string Desctiption { get => ""; }

        // 优先级
        public int Priority { get => 0; }

        // 自动施放
        public bool IsAuto { get => false; }

        // 对自己施放
        public bool IsSelf { get => false; }

        // 通过建筑生成
        public IEffect CreateByBuilding(IBuilding building)
        {
            return null;
        }

        // 通过角色生成
        public IEffect CreateByCharacter(ICharacter character)
        {
            return null;
        }

        // 通过物品生成
        public IEffect CreateByItem(IItem item)
        {
            return null;
        }

    }

    class HitEffect : IEffect
    {
        protected IEffectClass mCls;

        protected string mID;

        protected int mDamage;

        protected bool mIsFinished = false;

        public HitEffect(IEffectClass cls, string id, int damage)
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

        // 是否可影响建筑
        public bool IsAppliableOnBuilding { get => false; }

        // 是否可影响角色
        public bool IsAppliableOnCharacter { get => true; }

        // 是否可影响道具
        public bool IsAppliableOnItem { get => false; }

        // 对应的效果类（角色能力/道具能力）
        public IEffectClass GetClass() { return this.mCls; }

        // 对建筑产生效果
        public void ApplyOnBuilding(IBuilding building) {}

        // 对角色产生效果
        public void ApplyOnCharacter(ICharacter character)
        {
            this.mIsFinished = true;
            var battle = character.GetBattle();
            if (battle == null)
            {
                return;
            }
            battle.DeferDamage(this.mCls.Series, this.mDamage);
        }

        // 对物品产生效果
        public void ApplyOnItem(IItem item) {}
    }
}
