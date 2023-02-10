namespace csbcgf
{
    public interface IStatContainer
    {
        /// <summary>
        /// Get all IStats.
        /// </summary>
        IDictionary<string, IList<IStat>> Stats { get; }

        /// <summary>
        /// Remove IStat with key.
        /// </summary>
        bool RemoveStat(string key, IStat stat);

        /// <summary>
        /// Get IStat with key.
        /// </summary>
        void AddStat(string key, IStat stat);

        /// <summary>
        /// Value of this IStatContainer.
        /// </summary>
        int GetValue(string key);

        /// <summary>
        /// Base value of this IStatContainer.
        /// </summary>
        int GetBaseValue(string key);
    }
}
