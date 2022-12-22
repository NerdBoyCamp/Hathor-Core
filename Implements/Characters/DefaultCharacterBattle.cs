using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;
        protected Dictionary<string, int> mHeals = new Dictionary<string, int>();
        protected Dictionary<string, int> mDamages = new Dictionary<string, int>();

        public int mHP = 0;
        public int mMaxHP = 0;
        public int mMaxHPExtra = 0;
        public int mAP = 0;
        public int mMaxAP = 0;
        public int mMaxAPExtra = 0;
        public int mStrength = 0;
        public int mStrengthExtra = 0;
        public int mIntelligence = 0;
        public int mIntelligenceExtra = 0;
        public int mDexterity = 0;
        public int mDexterityExtra = 0;
        public Dictionary<string, int> mResistance = new Dictionary<string, int>();
        public Dictionary<string, int> mResistanceExtra = new Dictionary<string, int>();

        public DefaultCharacterBattle(ICharacter character) {
            this.mChar = character;
        }

        protected int UpdateHeal() {
            var value = 0;
            foreach(var heal in this.mHeals) {
                value += heal.Value;
            }
            this.mHeals.Clear();
            return value;
        }

        protected int UpdateDamage() {
            var value = 0;
            foreach(var damage in this.mDamages) {
                if (value <= 0) {
                    continue;
                }

                value += damage.Value;
                this.mChar.Publish(new BattleDamageEvent{
                    Character = this.mChar,
                    Damage = value
                });
            }
            this.mDamages.Clear();
            return value;
        }

        // 血量
        public int HP { get => this.mHP; }

        // 最大血量
        public int MaxHP { get => this.mMaxHP + this.mMaxHPExtra; }

        // 怒气
        public int AP { get => this.mAP; }

        // 最大怒气
        public int MaxAP { get => this.mMaxAP + this.mMaxAPExtra; }

        // 力量
        public int Strength { get => this.mStrength + this.mStrengthExtra; }

        // 智力
        public int Intelligence { get => this.mIntelligence + this.mIntelligenceExtra; }

        // 敏捷
        public int Dexterity { get => this.mDexterity + this.mDexterityExtra; }

        // 延时回复计算
        public void DeferHeal(string series, int value) {
            int prevValue = 0;
            bool hasValue = this.mHeals.TryGetValue(series, out prevValue);
            int curValue = Math.Max(prevValue + value, 0);
            if (curValue != 0 || hasValue) {
                this.mHeals[series] = curValue;
            }
        }

        public void DeferHealUp(string series, float value) {
            int prevValue = 0;
            bool hasValue = this.mHeals.TryGetValue(series, out prevValue);
            int curValue = Math.Max((int)((float)prevValue * value), 0);
            if (curValue != 0 || hasValue) {
                this.mHeals[series] = curValue;
            }
        }

        // 延时伤害计算
        public void DeferDamage(string series, int value) {
            int prevValue = 0;
            bool hasValue = this.mDamages.TryGetValue(series, out prevValue);
            int curValue = Math.Max(prevValue + value, 0);
            if (curValue != 0 || hasValue) {
                this.mDamages[series] = curValue;
            }
        }
        
        public void DeferDamageUp(string series, float value) {
            int prevValue = 0;
            bool hasValue = this.mDamages.TryGetValue(series, out prevValue);
            int curValue = Math.Max((int)((float)prevValue * value), 0);
            if (curValue != 0 || hasValue) {
                this.mDamages[series] = curValue;
            }
        }

        public void FlushDamage() {
            // 更新血量 -------------------------------------
            var HPDelta = this.UpdateHeal() - this.UpdateDamage();
            if (this.mHP >= this.MaxHP) {
                // 如果血量已满或超过上限，就不加血了
                if (HPDelta > 0) {
                    HPDelta = 0;
                }
            }
            this.mHP += HPDelta;
            this.mHP = Math.Max(this.mHP, 0);
        }
    }
}
