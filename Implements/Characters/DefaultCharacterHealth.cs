using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterHealthBuffer : ICharacterHealthBuffer
    {
        protected float mValue;

        public DefaultCharacterHealthBuffer()
        {
            this.mValue = 0;
        }

        public float Value { get => this.mValue; }

        // 提升数值
        public void Increase(float value)
        {
            this.mValue += value;
        }

        // 扩大提升数值
        public void Expend(float value)
        {
            this.mValue = this.mValue * value;
        }
    }

    class DefaultCharacterHealth : ICharacterHealth
    {
        protected ICharacter mChar;
        protected IAttribute mMaxHP;
        protected float mHP;
        protected Dictionary<string, ICharacterHealthBuffer> mHealingBuffers;
        protected Dictionary<string, ICharacterHealthBuffer> mDamageBuffers;

        public DefaultCharacterHealth(
            ICharacter character,
            IAttribute maxHp,
            float hp
        )
        {
            this.mChar = character;
            this.mMaxHP = maxHp;
            this.mHP = hp;
            this.mHealingBuffers = new Dictionary<string, ICharacterHealthBuffer>();
            this.mDamageBuffers = new Dictionary<string, ICharacterHealthBuffer>();
        }

        protected float FlushHealingBuffer()
        {
            if (this.mHealingBuffers.Count == 0)
            {
                return 0;
            }

            float value = 0;
            foreach (var heal in this.mHealingBuffers)
            {
                float healValue = heal.Value.Value;
                if (healValue <= 0)
                {
                    continue;
                }

                value += healValue;
            }
            this.mHealingBuffers.Clear();
            return value;
        }

        protected float FlushDamageBuffer()
        {
            if (this.mDamageBuffers.Count == 0)
            {
                return 0;
            }

            float value = 0;
            foreach (var damage in this.mDamageBuffers)
            {
                float damageValue = damage.Value.Value;
                if (damageValue <= 0)
                {
                    continue;
                }

                value += damageValue;
                this.mChar.Publish(new BattleDamageEvent
                {
                    Character = this.mChar,
                    Damage = value
                });
            }
            this.mDamageBuffers.Clear();
            return value;
        }

        protected float GetValue()
        {
            // 更新血量 -------------------------------------
            float HPDelta = this.FlushHealingBuffer() - this.FlushDamageBuffer();
            if (HPDelta != 0) // 没有变化就不用更新了
            {
                if (this.mHP >= this.mMaxHP.Value)
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
            return this.mHP;
        }

        // 血量值
        public float Value { get => this.GetValue(); }

        // 回复计算
        public ICharacterHealthBuffer GetHealingBuffer(string series)
        {
            ICharacterHealthBuffer buffer = null;
            this.mHealingBuffers.TryGetValue(series, out buffer);
            if (buffer == null)
            {
                buffer = new DefaultCharacterHealthBuffer();
                this.mHealingBuffers.Add(series, buffer);
            }
            return buffer;
        }

        // 伤害计算
        public ICharacterHealthBuffer GetDamageBuffer(string series)
        {
            ICharacterHealthBuffer buffer = null;
            this.mDamageBuffers.TryGetValue(series, out buffer);
            if (buffer == null)
            {
                buffer = new DefaultCharacterHealthBuffer();
                this.mDamageBuffers.Add(series, buffer);
            }
            return buffer;
        }
    }
}