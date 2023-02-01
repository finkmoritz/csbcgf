namespace csbcgf
{
    public abstract class Reaction<T> : IReaction where T : IAction
    {
        public void ReactBefore(IGame game, IAction action) {
            if(action is T) {
                T a = (T)action;
                ReactBeforeInternal(game, a);
            }
        }

        public void ReactAfter(IGame game, IAction action) {
            if(action is T) {
                T a = (T)action;
                ReactAfterInternal(game, a);
            }
        }

        protected virtual void ReactBeforeInternal(IGame game, T action) {}

        protected virtual void ReactAfterInternal(IGame game, T action) {}
    }
}
