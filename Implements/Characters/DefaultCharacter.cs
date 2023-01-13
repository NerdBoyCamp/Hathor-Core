using System;

namespace Hathor
{
    class DefaultCharacterConfig
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int AP { get; set; }
        public int MaxAP { get; set; }
        public int EXP { get; set; }
        public int LV { get; set; }
        public int AvailablePoints { get; set; }
        public int Perception { get; set; }
        public int Luck { get; set; }
        public int Eloquence { get; set; }
        public int Appearance { get; set; }
        public int Strength { get; set; }

    }

    class DefaultCharacterClass : ICharacterClass
    {
        public string ID { get => "fZ4B8dNd6U+s"; }

        // 名字
        public string Name { get => "Default Character"; }

        // 描述
        public string Desctiption { get => this.Name; }

        public ICharacter Create(object configs)
        {
            var configObject = configs as DefaultCharacterConfig;
            if (configObject == null)
            {
                return null;
            }
            return null;
        }
    }

    class DefaultCharacter : ICharacter
    {
        // 所属类别
        protected ICharacterClass mCls;

        // ID
        protected string mID;

        // Name
        protected string mName;

        // 监听者
        public IEventListener mListener = null;

        // 角色属性
        public ICharacterAdventure mAttributes = null;

        // 角色战斗
        public ICharacterBattle mBattle = null;

        // 角色成长
        public ICharacterUpgrade mGrowth = null;

        // 角色身上效果
        public IEffects mEffects = null;

        // 角色能力
        public IEffectAbilities mAbilities = null;

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
        public DefaultCharacter(ICharacterClass Cls, string ID)
        {
            this.mCls = Cls;
            this.mID = ID;
        }

        // 对应的角色类
        public ICharacterClass GetClass() { return this.mCls; }

        // 当前角色属性
        public ICharacterAdventure GetAdventure() { return this.mAttributes; }

        // 当前角色战斗
        public ICharacterBattle GetBattle() { return this.mBattle; }

        // 当前角色成长
        public ICharacterUpgrade GetUpgrade() { return this.mGrowth; }

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        public IEffects GetEffects() { return this.mEffects; }

        // 当前角色能力/技能
        public IEffectAbilities GetAbilities() { return this.mAbilities; }

        // 当前角色装备
        public ICharacterEquipments GetEquipments() { return this.mEquipments; }

        // 当前角色的背包
        public ICharacterStores GetStores() { return this.mStores; }

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

        // 更新角色/每帧调用
        public void Update()
        {
            if (this.mEffects != null)
            {
                this.mEffects.Update();
            }

            if (this.mEquipments != null)
            {
                this.mEquipments.Update();
            }

            if (this.mBattle != null)
            {
                this.mBattle.Update();
            }
        }
    }
}
