using System;
namespace csccgl
{
    public interface ICharacter
    {
        /// <summary>
        /// Maximum damage that can be taken until this Monster dies.
        /// </summary>
        LifeStat LifeStat { get; }

        /// <summary>
        /// Damage that is dealt to an enemy in battle.
        /// </summary>
        AttackStat AttackStat { get; }

        /// <summary>
        /// Checks if this Character is still alive.
        /// </summary>
        /// <returns>True if this Character is alive.</returns>
        bool IsAlive();
    }
}
