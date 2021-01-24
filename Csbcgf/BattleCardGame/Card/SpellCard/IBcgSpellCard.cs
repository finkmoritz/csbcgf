namespace Csbcgf.BattleCardGame
{
    public interface ISpellCard : ICard
    {
        bool IsCastable(IGameState gameState);
    }
}
