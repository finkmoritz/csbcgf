using System;
using System.Collections.Generic;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgTargetful : ICloneable
    {
        /// <summary>
        /// Get a set of potential target Characters based on the current
        /// state of the Game.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>All valid target Characters.</returns>
        HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState);
    }
}
