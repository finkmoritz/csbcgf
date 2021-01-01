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
        [JsonConstructor]
        public AttackStat(int value) : base(value, value)
        {
        }
    }
}
