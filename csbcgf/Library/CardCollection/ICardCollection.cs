using System.Collections.Generic;

namespace csbcgf
{
    public interface ICardCollection
    {
        /// <summary>
        /// Get all cards in this Deck.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Number of Cards currently in this Deck.
        /// </summary>
        /// <returns>The number of Cards in this Deck.</returns>
        int Size { get; }

        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Checks if this Deck contains the given Card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if this Deck contains the given Card.</returns>
        bool Contains(ICard card);
    }
}
