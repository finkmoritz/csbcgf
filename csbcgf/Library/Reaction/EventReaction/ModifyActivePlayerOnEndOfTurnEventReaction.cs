using System;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyActivePlayerOnEndOfTurnEventReaction : IReaction
    {
        [JsonConstructor]
        public ModifyActivePlayerOnEndOfTurnEventReaction()
        {
        }

        public object Clone()
        {
            return new ModifyActivePlayerOnEndOfTurnEventReaction();
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(EndOfTurnEvent)))
            {
                int playerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
                playerIndex = (playerIndex + 1) % game.Players.Count;
                game.Execute(new ModifyActivePlayerAction(game.Players[playerIndex]));
            }
        }
    }
}
