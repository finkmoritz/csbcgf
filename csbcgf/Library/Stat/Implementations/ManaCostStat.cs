namespace csbcgf {

    public class ManaCostStat : Stat
    {
        protected ManaCostStat() {}
        
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        public ManaCostStat(int value, int baseValue) : base(value, baseValue)
        {
        }
    }
}
