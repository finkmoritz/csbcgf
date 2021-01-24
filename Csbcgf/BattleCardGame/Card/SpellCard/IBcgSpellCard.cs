namespace Csbcgf.BattleCardGame
{
    public interface IBcgSpellCard : IBcgCard
    {
        bool IsCastable(IBcgGameState gameState);
    }
}
