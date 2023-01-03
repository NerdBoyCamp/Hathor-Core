using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;
        protected int mHP;
        protected int mAP;
        protected Dictionary<string, ICharacterAttribute> mDamageBuffer;
        protected Dictionary<string, ICharacterAttribute> mHealingBuffer;

        protected Dictionary<string, ICharacterAttributeEx> mAttributes;

        public DefaultCharacterBattle(
            ICharacter character,
            Dictionary<string, int> attributes
        )
        {
            this.mChar = character;
            this.mDamageBuffer = new Dictionary<string, ICharacterAttribute>();
            this.mHealingBuffer = new Dictionary<string, ICharacterAttribute>();
            this.mAttributes = new Dictionary<string, ICharacterAttributeEx>();
            foreach (var attr in attributes)
            {
                this.mAttributes.Add(
                    attr.Key, new DefalutCharacterAttributeEx(attr.Value));
            }
            this.mHP = this.MaxHP.Value;
            this.mAP = this.MaxAP.Value;
        }

        protected int UpdateHealing()
        {
            var value = 0;
            foreach (var heal in this.mHealingBuffer)
            {
                var healValue = heal.Value;
                healValue.Update();
                if (healValue.Value <= 0)
                {
                    continue;
                }
                value += healValue.Value;
            }
            this.mHealingBuffer.Clear();
            return value;
        }

        protected int UpdateDamage()
        {
            var value = 0;
            foreach (var damage in this.mDamageBuffer)
            {
                var damageValue = damage.Value;
                damageValue.Update();
                if (damageValue.Value <= 0)
                {
                    continue;
                }

                value += damageValue.Value;
                this.mChar.Publish(new BattleDamageEvent
                {
                    Character = this.mChar,
                    Damage = value
                });
            }
            this.mDamageBuffer.Clear();
            return value;
        }

        // 血量
        public int HP { get => this.mHP; }

        // 怒气
        public int AP { get => this.mAP; }

        public ICharacterAttribute GetDamageBuffer(string series)
        {
            ICharacterAttribute buffer = null;
            this.mDamageBuffer.TryGetValue(series, out buffer);
            if (buffer == null)
            {
                buffer = new DefaultCharacterAttribute(0);
                this.mDamageBuffer.Add(series, buffer);
            }
            return buffer;
        }

        // 回复计算
        public ICharacterAttribute GetHealingBuffer(string series)
        {
            ICharacterAttribute buffer = null;
            this.mHealingBuffer.TryGetValue(series, out buffer);
            if (buffer == null)
            {
                buffer = new DefaultCharacterAttribute(0);
                this.mHealingBuffer.Add(series, buffer);
            }
            return buffer;
        }

        // 最大血量
        public ICharacterAttributeEx MaxHP { get => this.GetAttribute("MaxHP"); }

        // 最大怒气
        public ICharacterAttributeEx MaxAP { get => this.GetAttribute("MaxAP"); }

        // 力量
        public ICharacterAttributeEx Strength { get => this.GetAttribute("Str"); }

        // 智力
        public ICharacterAttributeEx Intelligence { get => this.GetAttribute("Int"); }

        // 敏捷
        public ICharacterAttributeEx Dexterity { get => this.GetAttribute("Dex"); }

        // 获取额外属性值
        public ICharacterAttributeEx GetAttribute(string attrName)
        {
            ICharacterAttributeEx value = null;
            this.mAttributes.TryGetValue(attrName, out value);
            return value;
        }

        public void Update()
        {
            // 更新血量 -------------------------------------
            var HPDelta = this.UpdateHealing() - this.UpdateDamage();
            if (HPDelta != 0) // 没有变化就不用更新了
            {
                if (this.mHP >= this.MaxHP.Value)
                {
                    // 如果血量已满或超过上限，就不加血了
                    if (HPDelta > 0)
                    {
                        HPDelta = 0;
                    }
                }
                this.mHP += HPDelta;
                this.mHP = Math.Max(this.mHP, 0);
            }

            foreach (var attr in this.mAttributes)
            {
                attr.Value.Update();
            }
        }
    }
}
