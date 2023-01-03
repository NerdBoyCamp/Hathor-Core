using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterEquipments : ICharacterEquipments
    {
        protected class ItemSlot
        {
            public IItem item;

            public string series;
        }

        // 所属角色
        protected ICharacter mChar;

        // 装备位
        protected Dictionary<string, ItemSlot> mSlots;

        // 构造
        public DefaultCharacterEquipments(ICharacter character)
        {
            this.mChar = character;
            this.mSlots = new Dictionary<string, ItemSlot>();
            this.mSlots["Head"] = new ItemSlot { item = null, series = "Helmet" };
            this.mSlots["RightHand"] = new ItemSlot { item = null, series = "Weapon" };
            this.mSlots["LeftHand"] = new ItemSlot { item = null, series = "Weapon" };
            this.mSlots["Chest"] = new ItemSlot { item = null, series = "Armor" };
            this.mSlots["Waist"] = new ItemSlot { item = null, series = "Belt" };
            this.mSlots["Feet"] = new ItemSlot { item = null, series = "Shoes" };
        }

        // 头
        public IItem Head { get => this.GetItem("Head"); }

        // 右手
        public IItem RightHand { get => this.GetItem("RightHand"); }

        // 左手
        public IItem LeftHand { get => this.GetItem("LeftHand"); }

        // 胸
        public IItem Chest { get => this.GetItem("Chest"); }

        // 腰
        public IItem Waist { get => this.GetItem("Waist"); }

        // 脚
        public IItem Feet { get => this.GetItem("Feet"); }

        // 获取装备位上的物品
        public IItem GetItem(string slot)
        {
            ItemSlot itemSlot = null;
            this.mSlots.TryGetValue(slot, out itemSlot);
            return itemSlot == null ? null : itemSlot.item;
        }

        // 装备物品
        public IItem EquipItem(string slot, IItem item)
        {
            ItemSlot itemSlot = null;
            this.mSlots.TryGetValue(slot, out itemSlot);
            if (itemSlot == null)
            {
                return null;
            }

            IItem itemPrev = itemSlot.item;
            if (item == itemPrev)
            {
                return null;
            }

            if (item != null)
            {
                if (!item.IsUsable)
                {
                    return null;
                }

                IItemBattle battle = item.GetBattle();
                if (battle == null)
                {
                    return null;
                }

                if (!battle.SetUser(this.mChar))
                {
                    return null;
                }
            }

            if (itemPrev != null)
            {
                IItemBattle battle = item.GetBattle();
                if (battle != null)
                {
                    battle.SetUser(null);
                }
            }

            itemSlot.item = item;
            return item;
        }

        public IItem RemoveItem(string slot)
        {
            return this.EquipItem(slot, null);
        }

        public void Update()
        {
            foreach (var slot in mSlots.Values)
            {
                if (slot.item != null)
                {
                    slot.item.Update();
                }
            }
        }
    }
}
