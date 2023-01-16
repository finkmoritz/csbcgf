namespace csbcgf
{
    public interface ITargetfulSpellCard : ITargetful, ISpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        void Cast(IGame game, ICharacter target);
    }
}
