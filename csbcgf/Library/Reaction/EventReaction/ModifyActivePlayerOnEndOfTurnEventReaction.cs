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

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is EndOfTurnEvent)
            {
                int playerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
                playerIndex = (playerIndex + 1) % game.Players.Count;
                reactions.Add(new ModifyActivePlayerAction(game.Players[playerIndex]));
            }
            return reactions;
        }
    }
}
