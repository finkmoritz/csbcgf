using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class LifeStat : Stat
    {
        /// <summary>
        /// Maximum number of damage that can be taken.
        /// </summary>
        [JsonConstructor]
        public LifeStat(int value) : base(value, value)
        {
        }
    }
}
