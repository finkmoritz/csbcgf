namespace csbcgf
{
    public interface IAction
    {
        /// <summary>
        /// Check if this Action can be executed on the given Game state.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>True if this Action can be executed on the given Game state.</returns>
        bool IsExecutable(IGame game);

        /// <summary>
        /// Execute this Action in order to change the Game's state.
        /// Important: The Game state should only be changed within this method!
        /// </summary>
        /// <param name="game"></param>
        void Execute(IGame game);
    }
}
