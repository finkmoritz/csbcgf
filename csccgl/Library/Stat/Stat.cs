using System;

namespace csccgl
{
    [Serializable]
    public abstract class Stat : IStat
    {
        /// <summary>
        /// Minimum value of this Stat's Value.
        /// </summary>
        protected int MinValue;

        /// <summary>
        /// Maximum value of this Stat's Value.
        /// </summary>
        protected int MaxValue;

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="minValue">Minimum value of this Stat's Value.</param>
        /// <param name="maxValue">Maximum value of this Stat's Value.</param>
        public Stat(int value, int minValue, int maxValue)
        {
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int Value {
            get => Value;
            set {
                Value = Math.Max(MinValue, Math.Min(MaxValue, value));
            }
        }
    }
}
