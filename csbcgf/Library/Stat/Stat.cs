using System;

namespace csbcgf
{
    [Serializable]
    public abstract class Stat : IStat
    {
        public const int GlobalMin = -99;
        public const int GlobalMax = 99;

        protected int value;
        protected int baseValue;

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue"></param>
        public Stat(int value, int baseValue)
        {
            this.baseValue = baseValue;
            this.value = value;
        }

        public virtual int Value {
            get => value;
            set => this.value = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
        }

        public virtual int BaseValue
        {
            get => baseValue;
            set => baseValue = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
        }
    }
}
