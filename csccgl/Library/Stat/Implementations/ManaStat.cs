using System;

namespace csccgl {
    [Serializable]
    public class ManaStat : Stat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        public ManaStat(int value) : base(value, 0, 99)
        {
        }
    }
}
