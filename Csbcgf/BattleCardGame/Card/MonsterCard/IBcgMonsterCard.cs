namespace Csbcgf.BattleCardGame
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
        /// Checks if this MonsterCard can be summoned.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>True if this MonsterCard can be summoned.</returns>
        bool IsSummonable(IGameState gameState);

        /// <summary>
        /// Attack the given target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="target"></param>
        void Attack(IGame game, ICharacter target);
    }
}
