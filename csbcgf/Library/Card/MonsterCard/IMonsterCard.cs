﻿namespace csbcgf
{
    public interface IMonsterCard : ICard, ICharacter, ITargetful
    {
        /// <summary>
        /// Marks if this MonsterCard is ready to attack. Normally, MonsterCards
        /// are unable to attack during the same turn that they have been
        /// played. Also it only allows one attack per turn.
        /// </summary>
        bool IsReadyToAttack { get; set; }

        /// <summary>
        /// Attack the given target Character.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        void Attack(IGame gameState, ICharacter target);
    }
}
