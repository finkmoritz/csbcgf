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
        /// Start the next turn.
        /// </summary>
        void NextTurn();

        /// <summary>
        /// Queue an Action for execution. Changes on the Game state should
        /// only be performed via Actions queued through this method!
        /// </summary>
        /// <param name="action"></param>
        void Queue(IAction action);

        /// <summary>
        /// Execute all Actions in the queue.
        /// </summary>
        void Process();
    }
}
