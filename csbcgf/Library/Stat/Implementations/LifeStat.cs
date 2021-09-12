using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public LifeStat(int value) : this(value, value)
        {
        }

        [JsonConstructor]
        public LifeStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new LifeStat(Value, BaseValue);
        }
    }
}
