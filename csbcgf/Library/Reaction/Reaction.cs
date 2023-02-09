namespace csbcgf
{
    public abstract class Reaction<T, TGame, TAction> : IReaction<T, TGame, TAction>
        where T : IGameState
        where TGame : IGame<T>
        where TAction : IAction<T>
    {
        public virtual void ReactBefore(TGame game, TAction action)
        {
        }

        public virtual void ReactAfter(TGame game, TAction action)
        {
        }

        void IReaction.ReactBefore(IGame game, IAction action)
        {
            if (game is TGame g && action is TAction a)
            {
                ReactBefore(g, a);
            }
        }

        void IReaction.ReactAfter(IGame game, IAction action)
        {
            if (game is TGame g && action is TAction a)
            {
                ReactAfter(g, a);
            }
        }
    }
}
