using System;

namespace csbcgf {

    [Serializable]
    public class ManaCostStat : Stat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        public ManaCostStat(int value, int baseValue) : base(value, baseValue)
        {
        }
    }
}
