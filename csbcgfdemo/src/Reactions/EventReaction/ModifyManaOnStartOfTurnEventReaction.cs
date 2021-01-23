using System;

namespace Csbcgf.Core
{
    [Serializable]
    public class ModifyManaOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new ModifyManaOnStartOfTurnEventReaction();
        }

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
