namespace csbcgf
{
    public interface IMonsterCard : ICard, ICharacter, ITargetful
    {
        /// <summary>
        /// Marks if this MonsterCard is ready to attack. Normally, MonsterCards
        /// are unable to attack during the same turn that they have been
        /// played. Also it only allows one attack per turn.
        /// </summary>
        bool IsReadyToAttack { get; set; }

        bool IsSummonable(IGameState gameState);

        /// <summary>
        /// Attack the given target Character.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="target"></param>
        void Attack(IGame game, ICharacter target);
    }
}
