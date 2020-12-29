using System;

namespace csbcgf
{
    [Serializable]
    public abstract class Event : IAction
    {
        public Event()
        {
        }

        public void Execute(IGame game)
        {
            //An event should not alter the game state.
        }

        public virtual bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
