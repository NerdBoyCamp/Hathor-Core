using System;

namespace Hathor
{
    class Player : ICharacter
    {
        // 成员变量 -----------------------------------------
        // 所属类别
        protected ICharacterClass mCls;

        // ID
        protected string mID;

        // Name
        protected string mName;

        // 监听者
        protected IEventListener mListener = null;

        // 角色属性
        public ICharacterAttributes mAttributes = null;

        // 角色战斗
        public ICharacterBattle mBattle = null;

        // 角色成长
        public ICharacterGrowth mGrowth = null;

        // 角色身上效果
        public ICharacterEffects mEffects = null;

        // 角色能力
        public ICharacterAbilities mAbilities = null;

        // 角色装备
        public ICharacterEquipments mEquipments = null;

        // 角色背包
        public ICharacterStores mStores = null;

        // --------------------------------------------------
        // ID
        public string ID { get => mID; }

        // 名字
        public string Name { get => mName; set => mName = value; }

        // 构造
        public Player(ICharacterClass Cls, string ID) {
            this.mCls = Cls;
            this.mID = ID;
        }

        // 对应的角色类
        public ICharacterClass GetClass() { return this.mCls; }

        // 当前角色属性
        public ICharacterAttributes GetAttributes() { return this.mAttributes; }

        // 当前角色战斗
        public ICharacterBattle GetBattle() { return this.mBattle; }

        // 当前角色成长
        public ICharacterGrowth GetGrowth() { return this.mGrowth; }

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        public ICharacterEffects GetEffects() { return this.mEffects; }

        // 当前角色能力/技能
        public ICharacterAbilities GetAbilities() { return this.mAbilities; }

        // 当前角色装备
        public ICharacterEquipments GetEquipments() { return this.mEquipments; }

        // 当前角色的背包
        public ICharacterStores GetStores() { return this.mStores; }

        public void Publish(IEvent ev) {
            if (this.mListener != null) {
                this.mListener.OnNotify(ev);
            }
        }

        public void Subscribe(IEventListener listener)
        {
            this.mListener = listener;
        }

        // 更新角色/每帧调用
        public void Update() { 
            if (this.mEffects != null) {
                this.mEffects.Update();
            }

            if (this.mBattle != null) {
                this.mBattle.Update();
            }
        }
    }
}
