using System;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyActivePlayerAction : IAction
    {
        [JsonProperty]
        public readonly IPlayer NewActivePlayer;

        [JsonConstructor]
        public ModifyActivePlayerAction(IPlayer newActivePlayer)
        {
            this.NewActivePlayer = newActivePlayer;
        }

        public void Execute(IGame game)
        {
            game.ActivePlayer = NewActivePlayer;
        }

        public bool IsExecutable(IGame gameState)
        {
            if(!gameState.Players.ToList().Contains(NewActivePlayer))
            {
                throw new CsbcgfException("Could not change the active " +
                    "player because the specified player is not involved " +
                    "in the game!");
            }
            return NewActivePlayer != gameState.ActivePlayer;
        }
    }
}
