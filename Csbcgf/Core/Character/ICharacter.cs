

using System;

namespace Csbcgf.Core
{
    public interface ICharacter : IManaful, IAttacking, ILiving, ICloneable
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
