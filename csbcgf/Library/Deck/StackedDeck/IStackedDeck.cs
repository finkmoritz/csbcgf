using System;
namespace csbcgf
{
    public interface IStackedDeck : IDeck
    {
        /// <summary>
        /// Remove and return the top Card from this Deck.
        /// </summary>
        /// <returns>The top Card from this Deck.</returns>
        ICard Pop();

        /// <summary>
        /// Push the given Card to this Deck.
        /// </summary>
        /// <param name="card"></param>
        void Push(ICard card);

        /// <summary>
        /// Shuffle this Deck.
        /// </summary>
        void Shuffle();
    }
}
