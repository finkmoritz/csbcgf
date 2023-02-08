namespace csbcgf
{
    public interface ICardReaction<T> : IReaction<T> where T : IAction
    {
        /// <summary>
        /// Returns the parent Card of this IReaction.
        /// </summary>
        ICard ParentCard { get; }
    }
}
