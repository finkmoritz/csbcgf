namespace csbcgf
{
    public interface IPlayerReaction<T> : IReaction<T> where T : IAction
    {
        /// <summary>
        /// Returns the parent IPlayer of this IReaction.
        /// </summary>
        IPlayer ParentPlayer { get; }
    }
}
