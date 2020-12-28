namespace csbcgf
{
    public interface ITargetfulSpellCard : ITargetful, ISpellCard
    {
        /// <summary>
        /// Play this SpellCard onto the target Character in order to alter
        /// the Game's state.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="targetCharacter"></param>
        void Play(IGame game, ICharacter targetCharacter);
    }
}
