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

        public List<IAction> ReactTo(IGame gameState, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is EndOfTurnEvent)
            {
                int playerIndex = gameState.Players.ToList().IndexOf(gameState.ActivePlayer);
                playerIndex = (playerIndex + 1) % gameState.Players.Count;
                reactions.Add(new ModifyActivePlayerAction(gameState.Players[playerIndex]));
            }
            return reactions;
        }
    }
}
