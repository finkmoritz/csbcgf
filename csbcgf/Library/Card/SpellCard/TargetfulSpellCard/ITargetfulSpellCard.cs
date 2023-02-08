namespace csbcgf
{
    public interface ITargetfulSpellCard<T> : ITargetful<T>, ISpellCard where T : IGameState
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="target"></param>
        void Cast(IGame<T> game, ICharacter target);
    }
}
