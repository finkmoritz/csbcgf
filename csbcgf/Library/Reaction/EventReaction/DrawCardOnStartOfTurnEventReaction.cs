namespace csbcgf
{
    public class DrawCardOnStartOfTurnEventReaction : Reaction<StartOfTurnEvent>
    {
        protected override void ReactAfterInternal(IGame game, StartOfTurnEvent action)
        {
            game.ActionQueue.Execute(new DrawCardAction(game.ActivePlayer));
        }
    }
}
