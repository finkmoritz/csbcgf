namespace Csbcgf.Core
{
    public interface IGame : IGameState, IReactive
    {
        /// <summary>
        /// Start the game.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Start the next turn.
        /// </summary>
        void NextTurn();

        /// <summary>
        /// Queue an Action for execution. Changes on the Game state should
        /// only be performed via Actions queued through this method!
        /// </summary>
        /// <param name="action"></param>
        void Execute(IAction action);
    }
}
