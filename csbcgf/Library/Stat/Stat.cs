using System;

namespace csbcgf
{
    [Serializable]
    public abstract class Stat : IStat
    {
        public const int GlobalMin = 0;
        public const int GlobalMax = 99;

        private int value;
        private int maxValue;

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="value">Initial value of this Stat.</param>
        /// <param name="maxValue">Maximum value this Stat's value can be set to.</param>
        public Stat(int value, int maxValue)
        {
            this.maxValue = maxValue;
            this.value = value;
        }

        public int Value {
            get => value;
            set {
                this.value = Math.Max(GlobalMin, Math.Min(maxValue, value));
            }
        }

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
            }
        }
    }
}
