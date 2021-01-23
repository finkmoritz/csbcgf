namespace Csbcgf.Core
{
    public interface ISpellCard : ICard
    {
        bool IsCastable(IGameState gameState);
    }
}
