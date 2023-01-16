namespace csbcgf
{
    public class DrawCardOnStartOfTurnEventReaction : Reaction
    {
        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                game.Execute(new DrawCardAction(game.ActivePlayer));
            }
        }
    }
}
