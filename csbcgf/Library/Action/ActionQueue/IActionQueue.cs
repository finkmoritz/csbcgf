using System.Collections.Generic;

namespace csbcgf
{
    public interface IActionQueue
    {
        /// <summary>
        /// Queue a given Action for later execution.
        /// </summary>
        /// <param name="action"></param>
        void Queue(IAction action);

        /// <summary>
        /// Queue all given Actions for later execution.
        /// </summary>
        /// <param name="actions"></param>
        void QueueAll(List<IAction> actions);

        /// <summary>
        /// Execute all Actions in the queue if they are executable
        /// (see IAction.IsExecutable). Actions that are not executable
        /// at this time will be discarded.
        /// </summary>
        /// <param name="game"></param>
        void Process(IGame game);
    }
}
