namespace Hathor
{
    class DefaultItem : IItem
    {
        protected IItemClass mCls;

        protected string mID;

        protected IEventListener mListener = null;

        public IItemBattle mBattle = null;

        public IEffects mEffects = null;

        public IAbilities mAbilities = null;

        public DefaultItem(IItemClass cls, string id)
        {
            this.mCls = cls;
            this.mID = id;
        }

        // ID
        public string ID { get => this.mID; }

        // 名字
        public string Name { get => this.mCls.Name; }

        // 是否可用
        public bool IsUsable { get => true; }

        // 是否用完
        public bool IsUsedUp { get => false; }

        // 道具类
        public IItemClass GetClass() { return this.mCls; }

        // 当前道具战斗相关
        public IItemBattle GetBattle() { return this.mBattle; }

        // 当前道具受到影响的效果（技能效果/抗性/buff/debuff）
        public IEffects GetEffects() { return this.mEffects; }

        // 当前道具拥有的可触发效果（能力/技能）
        public IAbilities GetAbilities() { return this.mAbilities; }

        public void Publish(IEvent ev)
        {
            if (this.mListener != null)
            {
                this.mListener.OnNotify(ev);
            }
        }

        public void Subscribe(IEventListener listener)
        {
            this.mListener = listener;
        }

        // 更新物品/每帧调用
        public void Update()
        {
            if (this.mEffects != null)
            {
                this.mEffects.Update();
            }
        }
    }
}
