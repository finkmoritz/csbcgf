namespace csbcgf
{
    public interface ITargetlessSpellCard<T> : ITargetless, ISpellCard where T : IGameState
    {
        /// <summary>
        /// Called when the spell card is cast.
        /// </summary>
        /// <param name="game"></param>
        void Cast(IGame<T> game);
    }
}
