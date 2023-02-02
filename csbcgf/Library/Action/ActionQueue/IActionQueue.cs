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
        /// Execute all specified Actions if they are executable
        /// (see IAction.IsExecutable). Actions that are not executable
        /// at this time will be discarded.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="actions"></param>
        void Execute(IGame game, List<IAction> actions);
    }
}
