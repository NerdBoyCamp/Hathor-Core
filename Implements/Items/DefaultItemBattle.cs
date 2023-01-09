using System.Collections.Generic;

namespace Hathor
{
    class DefaultItemBattle : IItemBattle
    {
        protected IItem mItem;

        protected ICharacter mUser = null;

        protected Dictionary<string, int> mAttrRequired = null;

        protected Dictionary<string, int> mAttrPromoted = null;

        protected Dictionary<string, ICharacterAttributeChange> mAttrChanged = null;

        public DefaultItemBattle(
            IItem item,
            Dictionary<string, int> requirments,
            Dictionary<string, int> promotions
        )
        {
            this.mItem = item;
            this.mAttrRequired = new Dictionary<string, int>(requirments);
            this.mAttrPromoted = new Dictionary<string, int>(promotions);
            this.mAttrChanged = new Dictionary<string, ICharacterAttributeChange>();
        }

        // 当前使用者
        public ICharacter User { get => this.mUser; }

        // 力量需求
        public int StrengthRequired { get => this.GetAttrValueRequired("Str"); }

        public int StrengthPromoted { get => this.GetAttrValuePromoted("Str"); }

        // 智力需求
        public int IntelligenceRequired { get => this.GetAttrValueRequired("Int"); }

        public int IntelligencePromoted { get => this.GetAttrValuePromoted("Int"); }

        // 敏捷需求
        public int DexterityRequired { get => this.GetAttrValueRequired("Dex"); }

        public int DexterityPromoted { get => this.GetAttrValuePromoted("Dex"); }

        // 获取额外属性需求值
        public int GetAttrValueRequired(string attr)
        {
            int value = 0;
            this.mAttrRequired.TryGetValue(attr, out value);
            return value;
        }

        // 获取额外属性提升值
        public int GetAttrValuePromoted(string attr)
        {
            int value = 0;
            this.mAttrPromoted.TryGetValue(attr, out value);
            return value;
        }

        // 当前使用角色的发挥度
        public float GetUserPerformance()
        {
            if (this.mUser == null)
            {
                return 0;
            }

            var battle = this.mUser.GetBattle();
            if (battle == null)
            {
                return 0;
            }

            // TODO: ...

            return 1;
        }

        public bool SetUser(ICharacter user)
        {
            if (this.mUser == user)
            {
                return true;
            }

            if (!this.mItem.IsUsable && user != null)
            {
                return false;
            }

            if (this.mUser != null)
            {
                // remove promotions
                var battle = this.mUser.GetBattle();
                if (battle != null)
                {
                    battle.PromoteClear(this.mItem.ID);
                }
            }

            if (user != null)
            {
                // add promotions
                var battle = user.GetBattle();
                if (battle != null)
                {
                    foreach (var attr in this.mAttrPromoted)
                    {
                        battle.Promote(attr.Key, this.mItem.ID, attr.Value);
                    }
                }
            }

            this.mUser = user;
            return true;
        }
    }
}
