using System;
using csbcgf;

namespace csccgl
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

        public abstract bool IsExecutable(IGame game);
    }
}
