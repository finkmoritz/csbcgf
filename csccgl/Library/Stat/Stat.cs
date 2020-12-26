using System;

namespace csccgl
{
    [Serializable]
    public abstract class Stat : IStat
    {
        public const int GlobalMin = 0;
        public const int GlobalMax = 99;

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="value">Initial value of this Stat.</param>
        /// <param name="maxValue">Maximum value this Stat's value can be set to.</param>
        public Stat(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public int Value {
            get => Value;
            set {
                Value = Math.Max(GlobalMin, Math.Min(MaxValue, value));
            }
        }

        public int MaxValue
        {
            get => MaxValue;
            set
            {
                MaxValue = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
            }
        }
    }
}
