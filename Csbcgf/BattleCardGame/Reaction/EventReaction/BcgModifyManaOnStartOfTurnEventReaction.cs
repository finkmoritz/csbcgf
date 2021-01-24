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
                IBcgPlayer activePlayer = (IBcgPlayer)game.ActivePlayer;
                int manaDelta = activePlayer.ManaBaseValue + 1 - activePlayer.ManaValue;
                game.Execute(new BcgModifyManaStatAction(activePlayer, manaDelta, 1));
            }
        }
    }
}
