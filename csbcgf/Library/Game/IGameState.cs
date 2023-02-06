namespace csbcgf
{
    public interface IGameState
    {
        /// <summary>
        /// Convenience method to retrieve the active Player.
        /// Equivalent to using
        /// <code>Players[ActivePlayerIndex]</code>
        /// </summary>
        IPlayer ActivePlayer { get; set; }

        /// <summary>
        /// Convenience method to retrieve the non-active Players.
        /// </summary>
        IEnumerable<IPlayer> NonActivePlayers { get; }

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
