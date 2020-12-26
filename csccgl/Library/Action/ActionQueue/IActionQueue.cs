using System;
namespace csccgl
{
    public interface IActionQueue
    {
        /// <summary>
        /// Queue an Action for later execution.
        /// </summary>
        /// <param name="action"></param>
        void Queue(IAction action);

        /// <summary>
        /// Execute all Actions in the queue if they are executable
        /// (see IAction.IsExecutable). Actions that are not executable
        /// at this time will be discarded.
        /// </summary>
        /// <param name="game"></param>
        void Process(IGame game);
    }
}
