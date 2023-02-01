namespace csbcgf
{
    public class ModifyActivePlayerOnEndOfTurnEventReaction : Reaction
    {
        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(EndOfTurnEvent)))
            {
                int playerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
                playerIndex = (playerIndex + 1) % game.Players.Count();
                game.Execute(new ModifyActivePlayerAction(game.Players.ElementAt(playerIndex)));
            }
        }
    }
}
