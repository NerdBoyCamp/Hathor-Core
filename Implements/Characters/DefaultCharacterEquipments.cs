using System.Linq;
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
            this.mSlots = new Dictionary<string, ItemSlot> {
                { "Head", new ItemSlot { item = null, series = "Helmet" } },
                { "Main", new ItemSlot { item = null, series = "Weapon" } },
                { "Chest", new ItemSlot { item = null, series = "Armor" } },
                { "Waist", new ItemSlot { item = null, series = "Belt" } },
                { "Feet", new ItemSlot { item = null, series = "Shoes" } },
            };
        }

        // 头
        public IItem Head { get => this.GetItem("Head"); }

        // 主手
        public IItem Main { get => this.GetItem("Main"); }

        // 胸
        public IItem Chest { get => this.GetItem("Chest"); }

        // 腰
        public IItem Waist { get => this.GetItem("Waist"); }

        // 脚
        public IItem Feet { get => this.GetItem("Feet"); }

        // 获取装备位上的物品
        public IItem GetItem(string slot)
        {
            this.mSlots.TryGetValue(slot, out ItemSlot itemSlot);
            return itemSlot?.item;
        }

        // 装备物品
        public IItem EquipItem(string slot, IItem item)
        {
            this.mSlots.TryGetValue(slot, out ItemSlot itemSlot);
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

                battle.User = this.mChar;
            }

            if (itemPrev != null)
            {
                IItemBattle battle = item.GetBattle();
                if (battle != null)
                {
                    battle.User = null;
                }
            }

            itemSlot.item = item;
            return item;
        }

        public IItem RemoveItem(string slot)
        {
            return this.EquipItem(slot, null);
        }

        public IItem[] ListItem()
        {
            return this.mSlots.Values.Where(
                slot => slot.item != null).Select(slot => slot.item).ToArray();
        }

        public void Update(float deltaTime)
        {
            foreach (var slot in mSlots.Values)
            {
                if (slot.item != null)
                {
                    slot.item.Update(deltaTime);
                }
            }
        }
    }
}
