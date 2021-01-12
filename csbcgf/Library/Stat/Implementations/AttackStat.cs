using System;
using Newtonsoft.Json;

namespace csbcgf
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
    }
}
