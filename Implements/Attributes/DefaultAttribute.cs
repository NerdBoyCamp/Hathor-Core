namespace Hathor
{
    abstract class BaseAttributeChange : IAttributeChange
    {
        protected DefaultAttribute mSource;
        protected BaseAttributeChange mPrev;
        protected BaseAttributeChange mNext;

        public BaseAttributeChange(
            DefaultAttribute source,
            BaseAttributeChange prev
        )
        {
            this.mSource = source;
            this.mPrev = prev;
            this.mNext = null;
        }

        public abstract float GetValue();

        public IAttribute Source { get => this.mSource; }

        // 增益后数值
        public float Value { get => this.GetValue(); }

        // 原始数值
        public float OriginValue
        {
            get => this.mPrev != null ? this.mPrev.Value : this.SourceOriginValue;
        }

        public float SourceOriginValue
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

    class IncreaseAttributeChange : BaseAttributeChange
    {
        protected float mValue;

        public IncreaseAttributeChange(
            DefaultAttribute source,
            BaseAttributeChange prev,
            float value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override float GetValue()
        {
            return this.OriginValue + this.mValue;
        }
    }

    class ExpandAttributeChange : BaseAttributeChange
    {
        protected float mValue;

        public ExpandAttributeChange(
            DefaultAttribute source,
            BaseAttributeChange prev,
            float value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override float GetValue()
        {
            return this.OriginValue * this.mValue;
        }
    }

    class ExpandIncreaseAttributeChange : BaseAttributeChange
    {
        protected float mValue;

        public ExpandIncreaseAttributeChange(
            DefaultAttribute source,
            BaseAttributeChange prev,
            float value
        ) : base(source, prev)
        {
            this.mValue = value;
        }

        public override float GetValue()
        {
            float DeltaValue = this.OriginValue - this.SourceOriginValue;
            DeltaValue = DeltaValue * this.mValue;
            return this.SourceOriginValue + DeltaValue;
        }
    }

    class DefaultAttribute : IAttribute
    {
        public float mValue;
        public float mOriginValue;
        public bool mIsDirty = true;
        public BaseAttributeChange mLatest = null;

        public DefaultAttribute(float value)
        {
            this.mValue = value;
            this.mOriginValue = value;
        }

        public float GetValue()
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

        public void SetOriginValue(float value)
        {
            this.mOriginValue = value;
            this.mIsDirty = true;
        }

        // 原始数值
        public float Value { get => this.GetValue(); }

        public float OriginValue
        {
            get => this.mOriginValue;
            set => this.SetOriginValue(value);
        }

        // 提升属性
        public IAttributeChange Increase(float value)
        {
            var latest = new IncreaseAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }

        // 扩大属性
        public IAttributeChange Expand(float value)
        {
            var latest = new ExpandAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }

        // 扩大提升的属性
        public IAttributeChange ExpendIncrease(float value)
        {
            var latest = new ExpandIncreaseAttributeChange(
                this, this.mLatest, value
            );
            this.mLatest = latest;
            return latest;
        }
    }
}
