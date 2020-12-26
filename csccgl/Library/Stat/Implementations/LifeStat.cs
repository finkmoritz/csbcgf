using System;

namespace csccgl
{
    [Serializable]
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public LifeStat(int value) : base(value, 0, 99)
        {
        }
    }
}
