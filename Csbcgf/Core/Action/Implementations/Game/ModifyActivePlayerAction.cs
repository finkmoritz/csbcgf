using System;
using System.Linq;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    public class ModifyActivePlayerAction : Action
    {
        [JsonProperty]
        public IPlayer NewActivePlayer;

        [JsonConstructor]
        public ModifyActivePlayerAction(IPlayer newActivePlayer, bool isAborted = false)
            : base(isAborted)
        {
            NewActivePlayer = newActivePlayer;
        }

        public override object Clone()
        {
            return new ModifyActivePlayerAction(
                null, // otherwise circular dependencies
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            game.ActivePlayer = NewActivePlayer;
        }

        public override bool IsExecutable(IGameState gameState)
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
