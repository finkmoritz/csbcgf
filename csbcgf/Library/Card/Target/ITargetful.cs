﻿using System;
using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetful
    {
        /// <summary>
        /// Get a set of potential target Characters based on the current
        /// state of the Game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns>All valid target Characters.</returns>
        HashSet<ICharacter> GetPotentialTargets(IGame game);
    }
}