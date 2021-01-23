using System;
using System.Linq;

namespace csbcgf
{
    [Serializable]
    public class ModifyActivePlayerOnEndOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new ModifyActivePlayerOnEndOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(EndOfTurnEvent)))
            {
                int playerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
                playerIndex = (playerIndex + 1) % game.Players.Count;
                game.Execute(new ModifyActivePlayerAction(game.Players[playerIndex]));
            }
        }
    }
}
