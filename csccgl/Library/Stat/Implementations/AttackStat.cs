using System;

namespace csccgl
{
    [Serializable]
    public class AttackStat : Stat
    {
        /// <summary>
        /// Potential damage to be dealt.
        /// </summary>
        public AttackStat() : base(0, 99)
        {
        }
    }
}
