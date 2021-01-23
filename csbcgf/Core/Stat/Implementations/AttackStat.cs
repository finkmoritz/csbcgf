using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class AttackStat : Stat
    {
        /// <summary>
        /// Potential damage to be dealt.
        /// </summary>
        public AttackStat(int value) : this(value, value)
        {
        }

        [JsonConstructor]
        public AttackStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new AttackStat(Value, BaseValue);
        }
    }
}
