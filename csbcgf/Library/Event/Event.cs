using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Event : IAction
    {
        [JsonConstructor]
        public Event()
        {
        }

        public void Execute(IGame game)
        {
            // An event should not alter the game state.
        }

        public virtual bool IsExecutable(IGame gameState)
        {
            return true;
        }
    }
}
