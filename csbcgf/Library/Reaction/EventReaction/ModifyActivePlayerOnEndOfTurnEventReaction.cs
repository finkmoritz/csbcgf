namespace csbcgf
{
    public class ModifyActivePlayerOnEndOfTurnEventReaction : Reaction<EndOfTurnEvent>
    {
        protected override void ReactAfterInternal(IGame game, EndOfTurnEvent action)
        {
            int playerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
            playerIndex = (playerIndex + 1) % game.Players.Count();
            game.Execute(new ModifyActivePlayerAction(game.Players.ElementAt(playerIndex)));
        }
    }
}
