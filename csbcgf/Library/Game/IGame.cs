using System.Collections.Generic;

namespace csbcgf
{
    public interface IGame
    {
        /// <summary>
        /// Convenience method to retrieve the active Player.
        /// Equivalent to using
        /// <code>Players[ActivePlayerIndex]</code>
        /// </summary>
        IPlayer ActivePlayer { get; }

        /// <summary>
        /// Convenience method to retrieve the non-active Player.
        /// Equivalent to using
        /// <code>Players[1 - ActivePlayerIndex]</code>
        /// </summary>
        IPlayer NonActivePlayer { get; }

        /// <summary>
        /// Array of Players involved in the Game.
        /// </summary>
        IPlayer[] Players { get; }

        /// <summary>
        /// Get all Cards involved in the Game.
        /// </summary>
        List<ICard> AllCards { get; }

        /// <summary>
        /// Convenience method to retrieve all Cards on the Boards of all players.
        /// </summary>
        List<ICard> AllCardsOnTheBoard { get; }

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

        /// <summary>
        /// Queue multiple Actions for execution. Changes on the Game state should
        /// only be performed via Actions queued through this method!
        /// </summary>
        /// <param name="actions"></param>
        void Execute(List<IAction> actions);
    }
}
