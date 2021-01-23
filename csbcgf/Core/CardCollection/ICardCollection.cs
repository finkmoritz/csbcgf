using System;
using System.Collections.Generic;

namespace Csbcgf.Core
{
    public interface ICardCollection : ICloneable
    {
        /// <summary>
        /// Get all cards in this collection.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Number of Cards currently in this collection.
        /// </summary>
        /// <returns>The number of Cards in this Deck.</returns>
        int Count { get; }

        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Add the specified card to this collection.
        /// </summary>
        /// <param name="card"></param>
        void Add(ICard card);

        /// <summary>
        /// Remove the specified card from this collection.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if the card was successfully removed from this
        /// collection.</returns>
        bool Remove(ICard card);

        /// <summary>
        /// Checks if this collection contains the given Card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if this collection contains the given Card.</returns>
        bool Contains(ICard card);
    }
}
