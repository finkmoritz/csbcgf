using System;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyActivePlayerAction : IAction
    {
        [JsonProperty]
        public IPlayer NewActivePlayer;

        [JsonConstructor]
        public ModifyActivePlayerAction(IPlayer newActivePlayer)
        {
            NewActivePlayer = newActivePlayer;
        }

        public void Execute(IGame game)
        {
            game.ActivePlayer = NewActivePlayer;
        }

        public bool IsExecutable(IGameState gameState)
        {
            if(!gameState.Players.Contains(NewActivePlayer))
            {
                throw new CsbcgfException("Could not change the active " +
                    "player because the specified player is not involved " +
                    "in the game!");
            }
            return NewActivePlayer != gameState.ActivePlayer;
        }
    }
}
