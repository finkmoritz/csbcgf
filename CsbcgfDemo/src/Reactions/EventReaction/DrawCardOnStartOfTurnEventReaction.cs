using System;

namespace Csbcgf.Core
{
    [Serializable]
    public class DrawCardOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new DrawCardOnStartOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                game.Execute(new DrawCardAction(game.ActivePlayer));
            }
        }
    }
}
