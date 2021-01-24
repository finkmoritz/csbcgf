using System;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyManaOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new BcgModifyManaOnStartOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                int manaDelta = game.ActivePlayer.ManaBaseValue + 1 - game.ActivePlayer.ManaValue;
                game.Execute(new BcgModifyManaStatAction(game.ActivePlayer, manaDelta, 1));
            }
        }
    }
}
