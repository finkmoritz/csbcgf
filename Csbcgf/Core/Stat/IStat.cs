namespace Csbcgf.Core
{
    public interface IStat
    {
        /// <summary>
        /// Atomic value of a Card's Stat.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Base value for this Stat.
        /// </summary>
        int BaseValue { get; set; }
    }
}
