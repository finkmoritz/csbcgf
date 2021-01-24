namespace Csbcgf.BattleCardGame
{
    public interface IBcgTargetlessSpellCard : IBcgTargetless, IBcgSpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="gameState"></param>
        void Cast(IBcgGame game);
    }
}
