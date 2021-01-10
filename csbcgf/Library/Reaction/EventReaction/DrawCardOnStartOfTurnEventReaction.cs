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

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                game.Execute(new DrawCardAction(game.ActivePlayer));
            }
        }
    }
}
