namespace csbcgf
{
    public interface IGameState : IReactive
    {
        /// <summary>
        /// List of Players involved in the Game.
        /// </summary>
        IEnumerable<IPlayer> Players { get; }

        /// <summary>
        /// Get all Cards involved in the Game.
        /// </summary>
        IEnumerable<ICard> Cards { get; }

        /// <summary>
        /// Add an IPlayer.
        /// </summary>
        void AddPlayer(IPlayer player);

        /// <summary>
        /// Remove an IPlayer.
        /// </summary>
        bool RemovePlayer(IPlayer player);
    }
}
