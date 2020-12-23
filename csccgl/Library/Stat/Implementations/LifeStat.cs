using System;

namespace csccgl
{
    [Serializable]
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        public LifeStat() : base(0, 99)
        {
        }
    }
}
