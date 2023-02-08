namespace csbcgf
{
    public interface IAction
    {
        /// <summary>
        /// Aborted Actions will not be executed.
        /// </summary>
        bool IsAborted { get; set; }

        /// <summary>
        /// Check if this Action can be executed on the given Game state.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>True if this Action can be executed on the given Game state.</returns>
        bool IsExecutable(IGameState gameState);

        /// <summary>
        /// Execute this Action in order to change the Game's state.
        /// </summary>
        /// <param name="game"></param>
        void Execute(IGame game);
    }

    public interface IAction<T> : IAction where T : IGameState
    {
        bool IsExecutable(T gameState);

        void Execute(IGame<T> game);
    }
}
