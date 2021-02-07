using System;
using System.Linq;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class BcgModifyActivePlayerOnEndOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new BcgModifyActivePlayerOnEndOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(EndOfTurnEvent)))
            {
                int playerIndex = game.Players.ToList().IndexOf(((SimpleBcgGame)game).ActivePlayer);
                playerIndex = (playerIndex + 1) % game.Players.Count;
                game.Execute(new ModifyActivePlayerAction(game.Players[playerIndex]));
            }
        }
    }
}
