using System;
using Newtonsoft.Json;

namespace csbcgf {

    [Serializable]
    public class ManaCostStat : Stat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        [JsonConstructor]
        public ManaCostStat(int value, int baseValue) : base(value, baseValue)
        {
        }
    }
}
