namespace csbcgf
{
    public interface IMonsterCard : ICard, ICharacter, ITargetful
    {
        bool IsSummonable(IGameState gameState);
    }
}
