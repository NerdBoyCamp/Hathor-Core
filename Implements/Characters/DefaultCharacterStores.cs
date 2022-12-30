using System;

namespace Hathor
{
    class DefaultCharacterStore : ICharacterStore
    {
        // 角色
        protected ICharacter mChar;

        // 名字
        protected string mName;

        // 容量
        protected int mCapacity;

        // 背包内容
        protected IItem[] mSlots;

        // 名字
        public string Name { get => this.mName; }

        // 获取包裹容量
        public int Capacity { get => this.mCapacity; }

        // 构造
        public DefaultCharacterStore(ICharacter character, string name, int capacity)
        {
            this.mChar = character;
            this.mName = name;
            this.mCapacity = capacity;
            this.mSlots = new IItem[capacity];
        }

        // 增加/减少包裹容量
        public bool ExtendCapacity(int slotCount)
        {
            if (slotCount == 0)
            {
                return true;
            }

            int newCapacity = this.mCapacity + slotCount;
            if (newCapacity < 0)
            {
                return false;
            }

            if (newCapacity == this.mCapacity)
            {
                // 没有变动
                return true;
            }

            if (slotCount < 0) {
                // 缩小背包
                for (int i = newCapacity; i < this.mCapacity; i++)
                {
                    // 检查是否有空间缩小背包
                    if (this.mSlots[i] != null) {
                        return false;
                    }
                }
                this.mCapacity = newCapacity;
                return true;

            } else {
                // 扩大背包
                IItem[] newSlots = new IItem[newCapacity];
                Array.Copy(this.mSlots, 0, newSlots, 0, this.Capacity);
                this.mSlots = newSlots;
                this.mCapacity = newCapacity;
                return true;

            }
        }

        // 交换位置
        public bool SwapItem(int slot1, int slot2)
        {
            if (slot1 <=0 || slot1 >= this.mCapacity)
            {
                return false;
            }
            if (slot2 <=0 || slot2 >= this.mCapacity)
            {
                return false;
            }
            if (slot1 == slot2)
            {
                return true;
            }
            IItem item = this.mSlots[slot1];
            this.mSlots[slot1] = this.mSlots[slot2];
            this.mSlots[slot2] = item;
            return true;
        }

        // 保存物品
        public IItem StoreItem(int slot, IItem item)
        {
            if (slot <=0 || slot >= this.mCapacity)
            {
                return null;
            }

            if (this.mSlots[slot] != null)
            {
                // 位置上已经有物品了
                return null;
            }

            // 查看是否物品已经在背包里了
            foreach (IItem i in this.mSlots)
            {
                if (i != null && i.ID == item.ID)
                {
                    return null;
                }
            }
            this.mSlots[slot] = item;
            return item;
        }

        // 丢弃物品
        public IItem DropItem(int slot)
        {
            if (slot <=0 || slot >= this.mCapacity)
            {
                return null;
            }

            IItem item = this.mSlots[slot];
            this.mSlots[slot] = null;
            return item;
        }

        // 查看物品
        public IItem GetItem(int slot)
        {
            if (slot <=0 || slot >= this.mCapacity)
            {
                return null;
            }
            return this.mSlots[slot];
        }
    }
}
