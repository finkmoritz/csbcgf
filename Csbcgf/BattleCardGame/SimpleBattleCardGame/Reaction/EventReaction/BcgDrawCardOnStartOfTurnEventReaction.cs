using System;
using Csbcgf.BattleCardGame;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class BcgDrawCardOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new BcgDrawCardOnStartOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                game.Execute(new DrawCardAction((SimpleBcgPlayer)((SimpleBcgGame)game).ActivePlayer));
            }
        }
    }
}
