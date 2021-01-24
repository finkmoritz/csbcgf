using System;
using System.Collections.Generic;

namespace Csbcgf.Core
{
    public interface ICardCollection : IList<ICard>, ICloneable
    {
        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty { get; }
    }
}
