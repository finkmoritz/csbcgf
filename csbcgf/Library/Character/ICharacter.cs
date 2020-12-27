using System;
namespace csbcgf
{
    public interface ICharacter : IOwnable
    {
        /// <summary>
        /// Indicates if this Character is still alive.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Maximum damage that can be taken until this Monster dies.
        /// </summary>
        LifeStat LifeStat { get; }

        /// <summary>
        /// Damage that is dealt to an enemy in battle.
        /// </summary>
        AttackStat AttackStat { get; }
    }
}
