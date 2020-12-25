using System;
using System.Collections.Generic;

namespace csccgl
{
    public interface ITargetful
    {
        /// <summary>
        /// Get a list of potential target Characters based on the current
        /// state of the Game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns>All valid target Characters.</returns>
        List<ICharacter> GetPotentialTargets(Game game);
    }
}
