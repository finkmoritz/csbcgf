

namespace csbcgf
{
    public interface ICharacter : IAttacking, ILiving
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
