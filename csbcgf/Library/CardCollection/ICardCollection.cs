namespace csbcgf
{
    public interface ICardCollection : IOwnable
    {
        /// <summary>
        /// Get all cards in this Deck.
        /// </summary>
        IEnumerable<ICard> Cards { get; }

        /// <summary>
        /// Number of Cards currently in this Deck.
        /// </summary>
        /// <returns>The number of Cards in this Deck.</returns>
        int Size { get; }

        /// <summary>
        /// Maximum number of Cards that this Hand can hold.
        /// </summary>
        int? MaxSize { get; set; }

        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Checks if this Deck has reached its maximum size.
        /// </summary>
        /// <returns>True if this Deck has reached its maximum size.</returns>
        bool IsFull { get; }

        /// <summary>
        /// Checks if this Deck contains the given Card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if this Deck contains the given Card.</returns>
        bool Contains(ICard card);

        /// <summary>
        /// Remove the specified Card from this CardCollection.
        /// </summary>
        /// <param name="card"></param>
        void Remove(ICard card);

        /// <summary>
        /// Add the given Card to this CardCollection.
        /// </summary>
        /// <param name="card"></param>
        void Add(ICard card);

        /// <summary>
        /// Get the Card at position index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The Card at position index.</returns>
        ICard this[int index] { get; }

        /// <summary>
        /// Get the first Card in this ICardCollection.
        /// </summary>
        /// <returns>The first Card in this ICardCollection.</returns>
        ICard First { get; }

        /// <summary>
        /// Get the last Card in this ICardCollection.
        /// </summary>
        /// <returns>The last Card in this ICardCollection.</returns>
        ICard Last { get; }

        /// <summary>
        /// Shuffle this CardCollection.
        /// </summary>
        void Shuffle();
    }
}
