namespace csbcgf
{
    public interface ISpellCard : ICard
    {
        bool IsCastable(IGameState gameState);
    }
}
