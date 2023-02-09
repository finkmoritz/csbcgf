namespace csbcgf
{
    public interface IPlayerReaction<T, TGame, TAction> : IReaction<T, TGame, TAction>
        where T : IGameState
        where TGame : IGame<T>
        where TAction : IAction<T>
    {
        /// <summary>
        /// Returns the parent IPlayer of this IReaction.
        /// </summary>
        IPlayer ParentPlayer { get; }
    }
}
