using System;
using System.Collections.Generic;
using System.Linq;

namespace csbcgf
{
    [Serializable]
    public class ModifyActivePlayerOnEndOfTurnEventReaction : IReaction
    {
        public ModifyActivePlayerOnEndOfTurnEventReaction()
        {
        }

        public List<IAction> ReactTo(IGame gameState, IActionEvent actionEvent)
        {
            List<IAction> reactions = new List<IAction>();
            if (actionEvent.IsAfter(typeof(EndOfTurnEvent)))
            {
                int playerIndex = gameState.Players.ToList().IndexOf(gameState.ActivePlayer);
                playerIndex = (playerIndex + 1) % gameState.Players.Count;
                reactions.Add(new ModifyActivePlayerAction(gameState.Players[playerIndex]));
            }
            return reactions;
        }
    }
}
