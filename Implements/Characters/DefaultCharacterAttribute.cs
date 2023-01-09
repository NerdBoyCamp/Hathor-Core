namespace Hathor
{
    abstract class BaseCharacterAttributeChange : ICharacterAttributeChange
    {
        protected DefaultCharacterAttribute mSource;
        protected BaseCharacterAttributeChange mPrev;
        protected BaseCharacterAttributeChange mNext;

        public BaseCharacterAttributeChange(
            DefaultCharacterAttribute source,
            BaseCharacterAttributeChange prev
        )
        {
            this.mSource = source;
            this.mPrev = prev;
            this.mNext = null;
        }

        public abstract int GetValue();

        public ICharacterAttribute Source { get => this.mSource; }

        // 增益后数值
        public int Value { get => this.GetValue(); }

        // 原始数值
        public int OriginValue
        {
            get => this.mPrev != null ? this.mPrev.Value : this.SourceOriginValue;
        }

        public int SourceOriginValue
        {
            get => this.mSource.OriginValue;
        }

        // 消除增益
        public void Dispel()
        {
            if (this.mPrev != null)
            {
                this.mPrev.mNext = this.mNext;
            }
            if (this.mNext != null)
            {
                this.mNext.mPrev = this.mPrev;
            }
            else
            {
                this.mSource.mLatest = this.mPrev;
            }
        }
    }

    class IncreaseCharacterAttributeChange : BaseCharacterAttributeChange
    {
        protected int mValue;

        public IncreaseCharacterAttributeChange(
            DefaultCharacterAttribute source,
            BaseCharacterAttributeChange prev,
            int value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override int GetValue()
        {
            return this.OriginValue + this.mValue;
        }
    }

    class ExpandCharacterAttributeChange : BaseCharacterAttributeChange
    {
        protected float mValue;

        public ExpandCharacterAttributeChange(
            DefaultCharacterAttribute source,
            BaseCharacterAttributeChange prev,
            float value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override int GetValue()
        {
            return (int)((float)this.OriginValue * this.mValue);
        }
    }

    class ExpandIncreaseCharacterAttributeChange : BaseCharacterAttributeChange
    {
        protected float mValue;

        public ExpandIncreaseCharacterAttributeChange(
            DefaultCharacterAttribute source,
            BaseCharacterAttributeChange prev,
            float value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override int GetValue()
        {
            int DeltaValue = this.OriginValue - this.SourceOriginValue;
            DeltaValue = (int)((float)DeltaValue * this.mValue);
            return this.SourceOriginValue + DeltaValue;
        }
    }

    class DefaultCharacterAttribute : ICharacterAttribute
    {
        public int mValue;
        public int mOriginValue;
        public bool mIsDirty = true;
        public BaseCharacterAttributeChange mLatest = null;

        public DefaultCharacterAttribute(int value)
        {
            this.mValue = value;
            this.mOriginValue = value;
        }

        public int GetValue()
        {
            if (!this.mIsDirty)
            {
                return this.mValue;
            }

            if (this.mLatest != null)
            {
                this.mValue = this.mLatest.Value;
            }
            else
            {
                this.mValue = this.mOriginValue;
            }

            this.mIsDirty = false;
            return this.mValue;
        }

        public void SetOriginValue(int value)
        {
            this.mOriginValue = value;
            this.mIsDirty = true;
        }

        // 原始数值
        public int Value { get => this.GetValue(); }

        public int OriginValue
        {
            get => this.mOriginValue;
            set => this.SetOriginValue(value);
        }

        // 提升属性
        public ICharacterAttributeChange Increase(int value)
        {
            var latest = new IncreaseCharacterAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }

        // 扩大属性
        public ICharacterAttributeChange Expand(float value)
        {
            var latest = new ExpandCharacterAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }

        // 扩大提升的属性
        public ICharacterAttributeChange ExpendIncrease(float value)
        {
            var latest = new ExpandIncreaseCharacterAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }
    }
}
