namespace csbcgf
{
    public interface ITargetlessSpellCard : ITargetless, ISpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="game"></param>
        void Cast(IGame game);
    }
}
