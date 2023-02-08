namespace csbcgf
{
    public interface IReaction
    {
        /// <summary>
        /// React before a given IAction.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        void ReactBefore(IGame game, IAction action);

        /// <summary>
        /// React after a given IAction.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        void ReactAfter(IGame game, IAction action);
    }

    public interface IReaction<T> : IReaction where T : IAction
    {
        /// <summary>
        /// React before a given IAction.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        void ReactBefore(IGame game, T action);

        /// <summary>
        /// React after a given IAction.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="action"></param>
        void ReactAfter(IGame game, T action);
    }
}
