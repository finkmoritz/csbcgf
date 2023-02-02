namespace csbcgf
{
    public interface IGame : IGameState, IReactive
    {
        /// <summary>
        /// Get the IGame's ActionQueue.
        /// </summary>
        IActionQueue ActionQueue { get; }

        /// <summary>
        /// Start the game.
        /// </summary>
        void Start();

        /// <summary>
        /// Start the next turn.
        /// </summary>
        void NextTurn();

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
