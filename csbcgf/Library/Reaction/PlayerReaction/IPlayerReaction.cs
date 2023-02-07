namespace csbcgf
{
    public interface IPlayerReaction : IReaction
    {
        /// <summary>
        /// Returns the parent IPlayer of this IReaction.
        /// </summary>
        IPlayer ParentPlayer { get; }
    }
}
