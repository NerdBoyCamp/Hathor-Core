namespace Hathor
{
    class DefaultCharacterAction : IAction
    {
        protected ICharacter mChar;

        protected IEffectClass mAbility;

        public DefaultCharacterAction(ICharacter character, IEffectClass ability)
        {
            this.mChar = character;
            this.mAbility = ability;
        }

        // 名字
        public string Name { get => this.mAbility.Name; }

        // 描述
        public string Desctiption { get => this.mAbility.Desctiption; }

        // 是否可对建筑施放
        public bool IsAppliableOnBuilding
        {
            get => this.mAbility.IsAppliableOnBuilding;
        }

        // 是否可对角色施放
        public bool IsAppliableOnCharacter
        {
            get => this.mAbility.IsAppliableOnCharacter && !this.mAbility.IsSelf;
        }

        // 是否可对道具施放
        public bool IsAppliableOnItem
        {
            get => this.mAbility.IsAppliableOnItem;
        }

        // 是否可直接释放
        public bool IsAppliable
        {
            get => this.mAbility.IsSelf;
        }

        // 对建筑施放
        public void ApplyOnBuilding(IBuilding building)
        {
            // var effect = this.mAbility.CreateByCharacter(this.mChar);
            // do nothing
        }

        // 对角色施放
        public void ApplyOnCharacter(ICharacter character)
        {
            var effects = character.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByCharacter(this.mChar);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }

        // 对物品施放
        public void ApplyOnItem(IItem item)
        {
            var effects = item.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByCharacter(this.mChar);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }

        // 直接施放
        public void Apply()
        {
            var effects = this.mChar.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByCharacter(this.mChar);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }
    }

    class DefaultItemAction : IAction
    {
        protected IItem mItem;

        protected IEffectClass mAbility;

        public DefaultItemAction(IItem item, IEffectClass ability)
        {
            this.mItem = item;
            this.mAbility = ability;
        }

        // 名字
        public string Name { get => this.mAbility.Name; }

        // 描述
        public string Desctiption { get => this.mAbility.Desctiption; }

        // 是否可对建筑施放
        public bool IsAppliableOnBuilding
        {
            get => this.mAbility.IsAppliableOnBuilding;
        }

        // 是否可对角色施放
        public bool IsAppliableOnCharacter
        {
            get => this.mAbility.IsAppliableOnCharacter;
        }

        // 是否可对道具施放
        public bool IsAppliableOnItem
        {
            get => this.mAbility.IsAppliableOnItem && !this.mAbility.IsSelf;
        }

        // 是否可直接释放
        public bool IsAppliable
        {
            get => this.mAbility.IsSelf;
        }

        // 对建筑施放
        public void ApplyOnBuilding(IBuilding building)
        {
            // do nothing
        }

        // 对角色施放
        public void ApplyOnCharacter(ICharacter character)
        {
            var effects = character.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByItem(this.mItem);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }

        // 对物品施放
        public void ApplyOnItem(IItem item)
        {
            var effects = item.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByItem(this.mItem);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }

        // 直接施放
        public void Apply()
        {
            var effects = this.mItem.GetEffects();
            if (effects == null)
            {
                return;
            }
            var effect = this.mAbility.CreateByItem(this.mItem);
            if (effect == null)
            {
                return;
            }

            effects.AddEffect(effect);
        }
    }
}