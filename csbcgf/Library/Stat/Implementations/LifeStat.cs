using System;

namespace csbcgf
{
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public LifeStat(int value) : this(value, value)
        {
        }

        public LifeStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new LifeStat(Value, BaseValue);
        }
    }
}
