using System;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgCharacter : IBcgManaful, IBcgAttacking, IBcgLiving, ICloneable
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
