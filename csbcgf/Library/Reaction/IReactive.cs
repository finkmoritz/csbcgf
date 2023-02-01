namespace csbcgf
{
    public interface IReactive : IReaction
    {
        /// <summary>
        /// Reactions of this IReactive.
        /// </summary>
        List<IReaction> Reactions { get; }

        /// <summary>
        /// All reactions of this IReactive including its children.
        /// </summary>
        /// <returns>All reactions of this IReactive including its children.</returns>
        List<IReaction> AllReactions();

        /// <summary>
        /// Add an IReaction.
        /// </summary>
        void AddReaction(IReaction reaction);

        /// <summary>
        /// Remove an IReaction.
        /// </summary>
        /// <returns>True if the specified IReaction was removed.</returns>
        bool RemoveReaction(IReaction reaction);
    }
}
