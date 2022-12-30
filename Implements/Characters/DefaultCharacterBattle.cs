using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;
        protected Dictionary<string, int> mHeals = new Dictionary<string, int>();
        protected Dictionary<string, int> mDamages = new Dictionary<string, int>();

        protected Dictionary<string, int> mAttributes = null;
        protected Dictionary<string, int> mAttributesRaw = null;
        protected int mHP = 0;
        protected int mAP = 0;

        public DefaultCharacterBattle(
            ICharacter character,
            Dictionary<string, int> attributes
        ) {
            this.mChar = character;
            this.mAttributes = new Dictionary<string, int>(attributes);
            this.mAttributesRaw = new Dictionary<string, int>(attributes);
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
        

        // 最大血量
        public int MaxHP { get => this.GetAttrValue("MaxHP"); }

        // 最大怒气
        public int MaxAP { get => this.GetAttrValue("MaxAP"); }

        // 力量
        public int Strength { get => this.GetAttrValue("Str"); }

        // 智力
        public int Intelligence { get => this.GetAttrValue("Int"); }

        // 敏捷
        public int Dexterity { get => this.GetAttrValue("Dex"); }

        // 获取额外属性值
        public int GetAttrValue(string attr)
        {
            int value = 0;
            this.mAttributes.TryGetValue(attr, out value);
            return value;
        }

        // 降低属性
        public void Demote(string attr, string id, int value)
        {
            // TODO: To be implemented
        }

        // 降低属性的增幅
        public void DemoteAmplify(string attr, string id, float value)
        {
            // TODO: To be implemented
        }

        // 提升属性
        public void Promote(string attr, string id, int value)
        {
            // TODO: To be implemented
        }

        // 提升属性的增幅
        public void PromoteAmplify(string attr, string id, float value)
        {
            // TODO: To be implemented
        }

        // 消除所有提升和降低
        public void PromoteClear(string id)
        {
            // TODO: To be implemented
        }

        // 血量
        public int HP { get => this.mHP; }

        // 怒气
        public int AP { get => this.mAP; }

        // 延时回复计算
        public void HealDefer(string series, int value) {
            int prevValue = 0;
            bool hasValue = this.mHeals.TryGetValue(series, out prevValue);
            int curValue = Math.Max(prevValue + value, 0);
            if (curValue != 0 || hasValue) {
                this.mHeals[series] = curValue;
            }
        }

        public void HealDeferAmplify(string series, float value) {
            int prevValue = 0;
            bool hasValue = this.mHeals.TryGetValue(series, out prevValue);
            int curValue = Math.Max((int)((float)prevValue * value), 0);
            if (curValue != 0 || hasValue) {
                this.mHeals[series] = curValue;
            }
        }

        // 延时伤害计算
        public void DamageDefer(string series, int value) {
            int prevValue = 0;
            bool hasValue = this.mDamages.TryGetValue(series, out prevValue);
            int curValue = Math.Max(prevValue + value, 0);
            if (curValue != 0 || hasValue) {
                this.mDamages[series] = curValue;
            }
        }
        
        public void DamageDeferAmplify(string series, float value) {
            int prevValue = 0;
            bool hasValue = this.mDamages.TryGetValue(series, out prevValue);
            int curValue = Math.Max((int)((float)prevValue * value), 0);
            if (curValue != 0 || hasValue) {
                this.mDamages[series] = curValue;
            }
        }

        public void DamageFlush() {
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
