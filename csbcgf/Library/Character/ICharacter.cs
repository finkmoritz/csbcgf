

using System;

namespace csbcgf
{
    public interface ICharacter : IManaful, IAttacking, ILiving
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
