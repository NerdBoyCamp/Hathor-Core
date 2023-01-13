using System.Collections.Generic;

namespace Hathor
{
    class DefaultItemBattleAttributes : IItemBattleAttributes
    {
        protected Dictionary<string, IAttribute> mAttributes;

        public DefaultItemBattleAttributes(Dictionary<string, int> attrs)
        {
            this.mAttributes = new Dictionary<string, IAttribute>();
            foreach (var attr in attrs)
            {
                this.mAttributes.Add(attr.Key, new DefaultAttribute(attr.Value));
            }
        }

        // 力量需求
        public IAttribute Strength { get => this.GetAttribute("Str"); }

        // 智力需求
        public IAttribute Intelligence { get => this.GetAttribute("Int"); }

        // 敏捷需求
        public IAttribute Dexterity { get => this.GetAttribute("Dex"); }

        // 获取额外属性需求值
        public IAttribute GetAttribute(string attrName)
        {
            IAttribute attr = null;
            this.mAttributes.TryGetValue(attrName, out attr);
            return attr;
        }

        public List<string> ListAttributeName()
        {
            return new List<string>(this.mAttributes.Keys);
        }
    }

    class DefaultItemBattle : IItemBattle
    {
        protected IItem mItem;

        protected DefaultItemBattleAttributes mRequirments;

        protected DefaultItemBattleAttributes mEnhancements;

        protected ICharacter mUser = null;

        protected float mUserPerformance = -1;

        protected List<IAttributeChange> mChanges;

        protected void ApplyUser(ICharacter user)
        {
            if (this.mUser == user)
            {
                return;
            }

            this.ApplyEnhancements(user);
            this.mUser = user;
            this.mUserPerformance = -1;  // 设置为-1表示需要重新计算
        }

        protected void ApplyEnhancements(ICharacter user)
        {
            foreach (var chg in this.mChanges)
            {
                chg.Dispel();
            }
            this.mChanges.Clear();

            if (user == null)
            {
                return;
            }

            var battle = user.GetBattle();
            if (battle == null)
            {
                return;
            }

            foreach (var attrName in this.mEnhancements.ListAttributeName())
            {
                var attr = this.mEnhancements.GetAttribute(attrName);
                var attrChar = battle.GetAttribute(attrName);
                if (attrChar != null)
                {
                    this.mChanges.Add(attrChar.Increase(attr.Value));
                }
            }
        }

        protected float GetUserPerformance()
        {
            if (this.mUser == null)
            {
                return -1;
            }

            if (this.mUserPerformance >= 0)
            {
                return this.mUserPerformance;
            }

            var battle = this.mUser.GetBattle();
            if (battle == null)
            {
                return -1;
            }

            float minPerf = 100;  // 最大发挥度
            foreach (var attrName in this.mRequirments.ListAttributeName())
            {
                var attr = this.mRequirments.GetAttribute(attrName);
                var attrChar = battle.GetAttribute(attrName);
                if (attrChar != null)
                {
                    minPerf = 0;
                }
                else
                {
                    float perf = (float)attrChar.OriginValue / (float)attr.Value;
                    if (perf < minPerf)
                    {
                        minPerf = perf;
                    }
                }
            }
            this.mUserPerformance = minPerf;
            return minPerf;
        }

        public DefaultItemBattle(
            IItem item,
            Dictionary<string, int> requirments,
            Dictionary<string, int> enhancements
        )
        {
            this.mItem = item;
            this.mRequirments = new DefaultItemBattleAttributes(requirments);
            this.mEnhancements = new DefaultItemBattleAttributes(enhancements);
            this.mChanges = new List<IAttributeChange>();
        }

        public IItemBattleAttributes Requirments { get => this.mRequirments; }

        // 属性的增益/减益
        public IItemBattleAttributes Enhancements { get => this.mEnhancements; }

        // 当前使用者
        public ICharacter User { get => this.mUser; set => this.ApplyUser(value); }

        // 当前使用角色的发挥度
        public float UserPerformance { get => this.GetUserPerformance(); }
    }
}
