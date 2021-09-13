using System;
using System.Collections.Generic;

namespace csbcgf
{
    public interface IActionQueue
    {
        /// <summary>
        /// Marks if the ActionQueue also executes IReactions to the
        /// executed IActions.
        /// Can be e.g. set to false during Game initialization.
        /// </summary>
        bool ExecuteReactions { get; set; }

        /// <summary>
        /// Execute all Actions in the queue if they are executable
        /// (see IAction.IsExecutable). Actions that are not executable
        /// at this time will be discarded.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        void Execute(IGame game, IAction action);
    }
}
