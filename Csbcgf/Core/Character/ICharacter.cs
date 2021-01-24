

using System;

namespace Csbcgf.Core
{
    public interface ICharacter : IBcgManaful, IBcgAttacking, IBcgLiving, ICloneable
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
