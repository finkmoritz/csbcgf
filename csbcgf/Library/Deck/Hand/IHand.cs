namespace csbcgf
{
    public interface IHand : IDeck
    {
        /// <summary>
        /// Maximum number of Cards that this Hand can hold.
        /// </summary>
        int MaxSize { get; }

        /// <summary>
        /// Remove the specified Card from this Hand.
        /// </summary>
        /// <param name="card"></param>
        void Remove(ICard card);

        /// <summary>
        /// Add the given Card to this Hand.
        /// </summary>
        /// <param name="card"></param>
        void Add(ICard card);

        /// <summary>
        /// Get the Card at position index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The Card at position index.</returns>
        ICard this[int index] { get; }
    }
}
