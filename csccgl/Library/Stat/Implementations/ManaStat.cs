using System;

namespace csccgl {
    [Serializable]
    public class ManaStat : Stat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        public ManaStat() : base(0, 99)
        {
        }
    }
}
