using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;
        protected ICharacterHealth mHP;
        protected ICharacterAnger mAP;
        protected Dictionary<string, IAttribute> mAttributes;

        public DefaultCharacterBattle(
            ICharacter character,
            Dictionary<string, float> attributes,
            float hp,
            float ap
        )
        {
            this.mChar = character;
            this.mAttributes = new Dictionary<string, IAttribute>();
            foreach (var attr in attributes)
            {
                this.mAttributes.Add(
                    attr.Key, new DefaultAttribute(attr.Value));
            }
            this.mHP = new DefaultCharacterHealth(this.mChar, this.MaxHP, hp);
            this.mAP = null;
        }

        // 血量
        public ICharacterHealth HP { get => this.mHP; }

        // 怒气
        public ICharacterAnger AP { get => this.mAP; }

        // 最大血量
        public IAttribute MaxHP { get => this.GetAttribute("MaxHP"); }

        // 最大怒气
        public IAttribute MaxAP { get => this.GetAttribute("MaxAP"); }

        // 力量
        public IAttribute Strength { get => this.GetAttribute("Str"); }

        // 智力
        public IAttribute Intelligence { get => this.GetAttribute("Int"); }

        // 敏捷
        public IAttribute Dexterity { get => this.GetAttribute("Dex"); }

        public IAttribute Speed { get => this.GetAttribute("Spd"); }

        // 暴击率
        public IAttribute CriticalHitRate { get => this.GetAttribute("CrtR"); }

        // 暴击伤害
        public IAttribute CriticalHitDamage { get => this.GetAttribute("CrtD"); }

        // 获取额外属性值
        public IAttribute GetAttribute(string attrName)
        {
            this.mAttributes.TryGetValue(attrName, out IAttribute value);
            return value;
        }

        public void Update()
        {
            // 刷新下血量
            if (this.mHP.Value <= 0)
            {
                // 死亡逻辑
            }
        }
    }
}
