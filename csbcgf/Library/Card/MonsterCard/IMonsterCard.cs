namespace csbcgf
{
    public interface IMonsterCard<T> : ICard, ICharacter, ITargetful<T> where T : IGameState
    {
        bool IsSummonable(T gameState);
    }
}
