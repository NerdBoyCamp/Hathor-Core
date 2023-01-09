using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterHealthBuffer : ICharacterHealthBuffer
    {
        protected int mValue;

        public DefaultCharacterHealthBuffer()
        {
            this.mValue = 0;
        }

        public int Value { get => this.mValue; }

        // 提升数值
        public void Increase(int value)
        {
            this.mValue += value;
        }

        // 扩大提升数值
        public void Expend(float value)
        {
            this.mValue = (int)((float)(this.mValue) * value);
        }
    }

    class DefaultCharacterHealth : ICharacterHealth
    {
        protected ICharacter mChar;
        protected ICharacterAttribute mMaxHP;
        protected int mHP;
        protected Dictionary<string, ICharacterHealthBuffer> mHealingBuffers;
        protected Dictionary<string, ICharacterHealthBuffer> mDamageBuffers;

        public DefaultCharacterHealth(
            ICharacter character,
            ICharacterAttribute maxHp,
            int hp
        )
        {
            this.mChar = character;
            this.mMaxHP = maxHp;
            this.mHP = hp;
            this.mHealingBuffers = new Dictionary<string, ICharacterHealthBuffer>();
            this.mDamageBuffers = new Dictionary<string, ICharacterHealthBuffer>();
        }

        protected int FlushHealingBuffer()
        {
            if (this.mHealingBuffers.Count == 0)
            {
                return 0;
            }

            int value = 0;
            foreach (var heal in this.mHealingBuffers)
            {
                int healValue = heal.Value.Value;
                if (healValue <= 0)
                {
                    continue;
                }

                value += healValue;
            }
            this.mHealingBuffers.Clear();
            return value;
        }

        protected int FlushDamageBuffer()
        {
            if (this.mDamageBuffers.Count == 0)
            {
                return 0;
            }

            int value = 0;
            foreach (var damage in this.mDamageBuffers)
            {
                int damageValue = damage.Value.Value;
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

        protected int GetValue()
        {
            // 更新血量 -------------------------------------
            int HPDelta = this.FlushHealingBuffer() - this.FlushDamageBuffer();
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
        public int Value { get => this.GetValue(); }

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