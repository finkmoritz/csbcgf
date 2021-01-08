using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class DrawCardOnStartOfTurnEventReaction : IReaction
    {
        public DrawCardOnStartOfTurnEventReaction()
        {
        }

        public List<IAction> ReactTo(IGame gameState, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is StartOfTurnEvent)
            {
                reactions.Add(new DrawCardAction(gameState.ActivePlayer));
            }
            return reactions;
        }
    }
}
