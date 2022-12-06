using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;

        protected int mHP = 0;

        protected int mAP = 0;

        protected int mMaxHP = 0;

        protected int mMaxAP = 0;

        protected Dictionary<string, int> mHeals = new Dictionary<string, int>();

        protected Dictionary<string, int> mDamages = new Dictionary<string, int>();

        public DefaultCharacterBattle(ICharacter character) {
            this.mChar = character;
        }

        protected int UpdateHeal(IEventListener listener) {
            var value = 0;
            foreach(var heal in this.mHeals) {
                value += heal.Value;
            }
            this.mHeals.Clear();
            return value;
        }

        protected int UpdateDamage(IEventListener listener) {
            var value = 0;
            foreach(var damage in this.mDamages) {
                value += damage.Value;
                if (listener != null) {
                    listener.OnNotify(new BattleDamageEvent{
                        Character = this.mChar,
                        Damage = value
                    });
                }
            }
            this.mDamages.Clear();
            return value;
        }

        // 血量
        public int HP { get => this.mHP; }

        // 最大血量
        public int MaxHP { get => this.mMaxAP; set => this.mMaxHP = value; }

        // 怒气
        public int AP { get => this.mAP; }

        // 最大怒气
        public int MaxAP { get => this.mMaxAP; set => this.mMaxAP = value; }

        // 延时回复计算
        public void DeferHeal(string series, int value) {
            int prevValue = 0;
            this.mHeals.TryGetValue(series, out prevValue);
            this.mHeals[series] = Math.Max(prevValue + value, 0);
        }

        public void DeferHealUp(string series, float value) {
            int prevValue = 0;
            this.mHeals.TryGetValue(series, out prevValue);
            this.mHeals[series] = Math.Max((int)((float)prevValue * value), 0);
        }

        // 延时伤害计算
        public void DeferDamage(string series, int value) {
            int prevValue = 0;
            this.mDamages.TryGetValue(series, out prevValue);
            this.mDamages[series] = Math.Max(prevValue + value, 0);
        }
        
        public void DeferDamageUp(string series, float value) {
            int prevValue = 0;
            this.mDamages.TryGetValue(series, out prevValue);
            this.mDamages[series] = Math.Max((int)((float)prevValue * value), 0);
        }

        public bool IncreaseAP(int value) {
            return false;
        }

        public bool DecreaseAP(int value) {
            return false;
        }

        public void Update(IEventListener listener) {
            // 更新血量 -------------------------------------
            var HPDelta = this.UpdateHeal(listener) - this.UpdateDamage(listener);
            if (this.mHP >= this.mMaxAP) {
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
