using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Event : Action
    {
        [JsonConstructor]
        public Event()
        {
        }

        public override void Execute(IGame game)
        {
            // An event should not alter the game state.
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
