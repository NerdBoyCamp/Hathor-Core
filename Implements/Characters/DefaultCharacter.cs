using System;
using System.Collections.Generic;

namespace Hathor
{
    // 用户配置字段，仅供参考。不会被使用
    class DefaultCharacterConfig
    {
        public string Name { get; set; }
        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float AP { get; set; }
        public float MaxAP { get; set; }
        public float EXP { get; set; }
        public float LV { get; set; }
        public float AvailablePoints { get; set; }
        public float Perception { get; set; }
        public float Luck { get; set; }
        public float Eloquence { get; set; }
        public float Appearance { get; set; }
        public float Strength { get; set; }
        public float Intelligence { get; set; }
        public float Dexterity { get; set; }
        public float Speed { get; set; }
        public float CriticalHitRate { get; set; }
        public float CriticalHitDamage { get; set; }
    }

    class DefaultCharacterClass : ICharacterClass
    {
        public string ID { get => "Character"; }

        // 名字
        public string Name { get => "Default Character"; }

        // 描述
        public string Desctiption { get => this.Name; }

        public ICharacter Create(object configs)
        {
            try
            {
                DefaultCharacter character =
                    new DefaultCharacter(this, Util.RandomID());
                character.Name = Util.GetConfigAsString(configs, "Name");
                character.mEffects = new DefaultCharacterEffects(character);
                character.mAbilities = new DefaultAbilities();
                character.mStores = new DefaultCharacterStores(character);
                character.mEquipments = new DefaultCharacterEquipments(character);
                character.mBattle = new DefaultCharacterBattle(
                    character,
                    new Dictionary<string, float> {
                        { "MaxHP", Util.GetConfigAsFloat(configs, "MaxHP") },
                        { "MaxAP", Util.GetConfigAsFloat(configs, "MaxAP") },
                        { "Str", Util.GetConfigAsFloat(configs, "Strength") },
                        { "Int", Util.GetConfigAsFloat(configs, "Intelligence") },
                        { "Dex", Util.GetConfigAsFloat(configs, "Dexterity") },
                        { "CrtRt", Util.GetConfigAsFloat(configs, "CriticalHitRate") },
                        { "CrtD", Util.GetConfigAsFloat(configs, "CriticalHitDamage") },
                    },
                    Util.GetConfigAsFloat(configs, "HP"),
                    Util.GetConfigAsFloat(configs, "AP")
                );
                return character;
            } 
            catch (Exception e)
            {
                // 任何异常都将创建失败
                Console.Error.WriteLine(e.ToString());
                return null;
            }
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
        public ICharacterAdventure mAdventure = null;

        // 角色战斗
        public ICharacterBattle mBattle = null;

        // 角色成长
        public ICharacterUpgrade mGrowth = null;

        // 角色身上效果
        public IEffects mEffects = null;

        // 角色能力
        public IAbilities mAbilities = null;

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
        public ICharacterAdventure GetAdventure() { return this.mAdventure; }

        // 当前角色战斗
        public ICharacterBattle GetBattle() { return this.mBattle; }

        // 当前角色成长
        public ICharacterUpgrade GetUpgrade() { return this.mGrowth; }

        // 当前角色受到影响的效果（技能效果/抗性/buff/debuff）
        public IEffects GetEffects() { return this.mEffects; }

        // 当前角色能力/技能
        public IAbilities GetAbilities() { return this.mAbilities; }

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
        public void Update(float deltaTime)
        {
            if (this.mEffects != null)
            {
                this.mEffects.Update(deltaTime);
            }

            if (this.mEquipments != null)
            {
                this.mEquipments.Update(deltaTime);
            }

            if (this.mBattle != null)
            {
                this.mBattle.Update(deltaTime);
            }
        }
    }
}
