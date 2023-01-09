using System;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterBattle : ICharacterBattle
    {
        protected ICharacter mChar;
        protected ICharacterHealth mHP;
        protected ICharacterAnger mAP;
        protected Dictionary<string, ICharacterAttribute> mAttributes;

        public DefaultCharacterBattle(
            ICharacter character,
            Dictionary<string, int> attributes,
            int hp,
            int ap
        )
        {
            this.mChar = character;
            this.mAttributes = new Dictionary<string, ICharacterAttribute>();
            foreach (var attr in attributes)
            {
                this.mAttributes.Add(
                    attr.Key, new DefaultCharacterAttribute(attr.Value));
            }
            this.mHP = new DefaultCharacterHealth(this.mChar, this.MaxHP, hp);
            this.mAP = null;
        }

        // 血量
        public ICharacterHealth HP { get => this.mHP; }

        // 怒气
        public ICharacterAnger AP { get => this.mAP; }

        // 最大血量
        public ICharacterAttribute MaxHP { get => this.GetAttribute("MaxHP"); }

        // 最大怒气
        public ICharacterAttribute MaxAP { get => this.GetAttribute("MaxAP"); }

        // 力量
        public ICharacterAttribute Strength { get => this.GetAttribute("Str"); }

        // 智力
        public ICharacterAttribute Intelligence { get => this.GetAttribute("Int"); }

        // 敏捷
        public ICharacterAttribute Dexterity { get => this.GetAttribute("Dex"); }

        // 获取额外属性值
        public ICharacterAttribute GetAttribute(string attrName)
        {
            ICharacterAttribute value = null;
            this.mAttributes.TryGetValue(attrName, out value);
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
