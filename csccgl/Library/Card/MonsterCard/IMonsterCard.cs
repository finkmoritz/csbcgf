using System;
namespace csccgl
{
    public interface IMonsterCard : ICard
    {
        /// <summary>
        /// Damage that is dealt to an enemy in battle.
        /// </summary>
        AttackStat AttackStat { get; }

        /// <summary>
        /// Maximum damage that can be taken until this Monster dies.
        /// </summary>
        LifeStat LifeStat { get; }
    }
}
