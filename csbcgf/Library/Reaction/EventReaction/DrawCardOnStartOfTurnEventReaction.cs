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

        public List<IAction> ReactTo(IGame gameState, IActionEvent actionEvent)
        {
            List<IAction> reactions = new List<IAction>();
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                reactions.Add(new DrawCardAction(gameState.ActivePlayer));
            }
            return reactions;
        }
    }
}
