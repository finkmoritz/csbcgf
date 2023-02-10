namespace csbcgf
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

        /// <summary>
        /// Minimum value for atomic and base value.
        /// </summary>
        int MinValue { get; set; }

        /// <summary>
        /// Maximum value for atomic and base value.
        /// </summary>
        int MaxValue { get; set; }
    }
}
