using System;
using System.Collections.Generic;

namespace Csbcgf.Core
{
    public interface IGameState : ICloneable
    {
        /// <summary>
        /// List of Players involved in the Game.
        /// </summary>
        List<IPlayer> Players { get; }

        /// <summary>
        /// Get all Cards involved in the Game.
        /// </summary>
        List<ICard> AllCards { get; }
    }
}
