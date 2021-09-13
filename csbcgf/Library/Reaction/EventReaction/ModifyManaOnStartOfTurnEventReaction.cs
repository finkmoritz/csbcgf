using System;

namespace csbcgf
{
    public class ModifyManaOnStartOfTurnEventReaction : Reaction
    {
        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                int manaDelta = game.ActivePlayer.ManaBaseValue + 1 - game.ActivePlayer.ManaValue;
                game.Execute(new ModifyManaStatAction(game.ActivePlayer, manaDelta, 1));
            }
        }
    }
}
