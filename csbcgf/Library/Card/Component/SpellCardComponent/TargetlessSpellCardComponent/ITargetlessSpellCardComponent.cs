namespace csbcgf
{
    public interface ITargetlessSpellCardComponent<T> : ISpellCardComponent, ITargetless where T : IGameState
    {
        /// <summary>
        /// Called when spell card is cast. Execute Actions here.
        /// </summary>
        /// <param name="gameState"></param>
        void Cast(IGame<T> game);
    }
}
