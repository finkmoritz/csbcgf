using System;
using System.Collections.Generic;

namespace Csbcgf.Core
{
    public interface IPlayer : IReactive, ICloneable
    {
        /// <summary>
        /// Get CardCollections of this Player.
        /// </summary>
        Dictionary<string, ICardCollection> CardCollections { get; }

        /// <summary>
        /// Get all Cards from the Player's CardCollections.
        /// </summary>
        List<ICard> AllCards { get; }
    }
}
