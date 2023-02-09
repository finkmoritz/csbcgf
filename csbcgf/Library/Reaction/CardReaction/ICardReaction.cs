namespace csbcgf
{
    public interface ICardReaction<T, TGame, TAction> : IReaction<T, TGame, TAction>
        where T : IGameState
        where TGame : IGame<T>
        where TAction : IAction<T>
    {
        /// <summary>
        /// Returns the parent Card of this IReaction.
        /// </summary>
        ICard ParentCard { get; }
    }
}
