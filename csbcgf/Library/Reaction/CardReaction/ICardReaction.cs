namespace csbcgf
{
    public interface ICardReaction : IReaction
    {
        /// <summary>
        /// Returns the parent Card of this IReaction.
        /// </summary>
        ICard ParentCard { get; }
    }
}
