using System;

namespace csbcgf {

    [Serializable]
    public class ManaStat : Stat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        public ManaStat(int value, int maxValue) : base(value, maxValue)
        {
        }
    }
}
