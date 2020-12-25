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
    }
}
