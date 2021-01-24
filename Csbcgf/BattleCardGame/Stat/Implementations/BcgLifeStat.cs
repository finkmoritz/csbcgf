using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgLifeStat : BcgStat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public BcgLifeStat(int value) : this(value, value)
        {
        }

        [JsonConstructor]
        public BcgLifeStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new BcgLifeStat(Value, BaseValue);
        }
    }
}
