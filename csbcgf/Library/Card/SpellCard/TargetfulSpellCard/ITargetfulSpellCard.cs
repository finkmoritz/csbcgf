namespace csbcgf
{
    public interface ITargetfulSpellCard : ITargetful, ISpellCard
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="target"></param>
        void Cast(IGame game, ICharacter target);
    }
}
