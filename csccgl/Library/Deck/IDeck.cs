using System;

namespace csccgl
{
    public interface IDeck
    {
        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty();

        /// <summary>
        /// Checks if this Deck contains the given Card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if this Deck contains the given Card.</returns>
        bool Contains(ICard card);

        /// <summary>
        /// Number of Cards currently in this Deck.
        /// </summary>
        /// <returns>The number of Cards in this Deck.</returns>
        int Size { get; }
    }
}
