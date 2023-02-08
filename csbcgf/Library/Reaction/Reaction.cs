namespace csbcgf
{
    public abstract class Reaction<T> : IReaction<T> where T : IAction
    {
        public virtual void ReactBefore(IGame game, T action)
        {
        }

        public virtual void ReactAfter(IGame game, T action)
        {
        }

        void IReaction.ReactBefore(IGame game, IAction action)
        {
            if (action is T a)
            {
                ReactBefore(game, a);
            }
        }

        void IReaction.ReactAfter(IGame game, IAction action)
        {
            if (action is T a)
            {
                ReactAfter(game, a);
            }
        }
    }
}
