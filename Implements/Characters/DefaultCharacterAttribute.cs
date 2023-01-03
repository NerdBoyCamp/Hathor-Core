namespace Hathor
{
    class DefaultCharacterAttribute : ICharacterAttribute
    {
        protected int mValue;
        protected int mValueDecrease;
        protected int mValueIncrease;

        public DefaultCharacterAttribute(int value)
        {
            this.mValue = value;
            this.mValueDecrease = 0;
            this.mValueIncrease = 0;
        }

        // 原始数值
        public int Value { get => this.mValue; }

        // 降低属性
        public void Decrease(int value)
        {
            this.mValueDecrease += value;
            if (this.mValueDecrease < 0)
            {
                this.mValueDecrease = 0;
            }
        }

        // 降低属性的增幅
        public void DecreaseAmplify(float value)
        {
            this.mValueDecrease = (int)(((float)this.mValueDecrease) * value);
            if (this.mValueDecrease < 0)
            {
                this.mValueDecrease = 0;
            }
        }

        // 提升属性
        public void Increase(int value)
        {
            this.mValueIncrease += value;
            if (this.mValueIncrease < 0)
            {
                this.mValueIncrease = 0;
            }
        }

        // 提升属性的增幅
        public void IncreaseAmplify(float value)
        {
            this.mValueIncrease = (int)(((float)this.mValueIncrease) * value);
            if (this.mValueIncrease < 0)
            {
                this.mValueIncrease = 0;
            }
        }

        public void Update()
        {
            if (this.mValueDecrease != 0)
            {
                this.mValue -= this.mValueDecrease;
                this.mValueDecrease = 0;
            }

            if (this.mValueIncrease != 0)
            {
                this.mValue += this.mValueIncrease;
                this.mValueIncrease = 0;
            }
        }
    }

    class DefalutCharacterAttributeEx : DefaultCharacterAttribute, ICharacterAttributeEx
    {
        protected int mOriginValue;

        public DefalutCharacterAttributeEx(int value) : base(value)
        {
            this.mOriginValue = value;
        }

        // 属性数值
        public int OriginValue { get => this.mOriginValue; }

        // 降低属性
        public void Reset()
        {
            this.mValue = this.mOriginValue;
        }
    }
}
