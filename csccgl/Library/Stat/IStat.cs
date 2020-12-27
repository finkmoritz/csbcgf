namespace csbcgf
{
    public interface IStat
    {
        /// <summary>
        /// Atomic value of a Card's Stat.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Maximum value of this Stat's Value.
        /// </summary>
        int MaxValue { get; set; }
    }
}
