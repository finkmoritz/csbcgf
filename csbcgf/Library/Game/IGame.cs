namespace csbcgf
{
    public interface IGame : IGameState, IReactive
    {
        /// <summary>
        /// Start the game.
        /// </summary>
        void Start();

        /// <summary>
        /// Start the next turn.
        /// </summary>
        void NextTurn();

        /// <summary>
        /// Queue an Action for execution. Changes on the Game state should
        /// only be performed via Actions queued through this method!
        /// </summary>
        /// <param name="action"></param>
        /// <param name="withReactions"></param>
        void Execute(IAction action, bool withReactions = true);

        /// <summary>
        /// Queue multiple Actions for execution. Changes on the Game state should
        /// only be performed via Actions queued through this method!
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="withReactions"></param>
        void Execute(List<IAction> actions, bool withReactions);

        /// <summary>
        /// Add an IPlayer.
        /// </summary>
        public void AddPlayer(IPlayer player);

        /// <summary>
        /// Remove an IPlayer.
        /// </summary>
        public bool RemovePlayer(IPlayer player);
    }
}
