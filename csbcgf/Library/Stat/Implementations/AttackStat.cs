using System;

namespace csbcgf
{
    [Serializable]
    public class AttackStat : Stat
    {
        /// <summary>
        /// Potential damage to be dealt.
        /// </summary>
        public AttackStat(int value) : base(value, 99)
        {
        }
    }
}
