using System;

namespace csbcgf
{
    [Serializable]
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public LifeStat(int value) : base(value, value)
        {
        }
    }
}
