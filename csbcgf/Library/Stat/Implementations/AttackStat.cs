namespace csbcgf
{
    public class AttackStat : Stat
    {
        protected AttackStat() {}

        /// <summary>
        /// Potential damage to be dealt.
        /// </summary>
        public AttackStat(int value) : this(value, value)
        {
        }

        public AttackStat(int value, int baseValue) : base(value, baseValue)
        {
        }
    }
}
