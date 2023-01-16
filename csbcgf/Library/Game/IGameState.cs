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
        List<IPlayer> NonActivePlayers { get; }

        /// <summary>
        /// List of Players involved in the Game.
        /// </summary>
        List<IPlayer> Players { get; }

        /// <summary>
        /// Get all Cards involved in the Game.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Convenience method to retrieve all Cards on the Boards of all players.
        /// </summary>
        List<ICard> AllCardsOnTheBoard { get; }
    }
}
